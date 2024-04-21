///Autor: Eargosha

import 'dart:math';
//свои модули:
import 'package:chat_bot/services/weather_service.dart';
import '../models/message.dart';
//необходимые подключения
import 'package:intl/intl.dart';
import 'package:intl/date_symbol_data_local.dart';
import 'dart:convert';
import 'package:path_provider/path_provider.dart';
import 'dart:io';
import 'dart:async';

///Класс чат бота, между классом ChatBot и Message(отдельный файл Message.dart) есть отношение композиции
///Поля:
///--Map<RegExp, Function> commands - словарь, в котором содержатся регулярные выражения(запросы пользователя) и делегаты(методы чат бота)
///--List<Message> allMessages - список всех сообщений, когда либо написанных пользователем и ботом, использует класс Message, создавая отношение композиции
///--String botName - имя бота, да можно менять, обратившись к нему через итерацию (мб)???(get, set)
///Методов много, обратитесь к словарю commands для их изучения
class ChatBot {
  //словарь, в котором содержатся регулярные выражения(запросы пользователя) и делегаты(методы чат бота)
  late Map<RegExp, Function> commands;
  //поле - список всех сообщенек, состоит из класса Message
  List<Message> allMessages = [];
  //имя бота, да можно менять
  String botName = '';

  ///Метод отправки сообщения в список всех сообщений
  void sendMessage(String userInput, String uName) {
    //если юзер не ввел ничего не делаем ничего
    if (userInput != '') {
      allMessages.add(
        Message(
          Future.value(userInput),
          uName,
          true,
          DateTime.now(),
        ),
      );
      botResponse(userInput);
    }
  }

//
  ///Метод реакции бота на отправленное сообщение пользователем
  void botResponse(String userInput) {
    //проверяем снова, если юзер не ввел ничего не делаем ничего
    if (userInput != '') {
      allMessages.add(
        Message(
          processCommand(userInput),
          botName,
          false,
          DateTime.now(),
        ),
      );
    }
  }

  ///Метод сохранения истории сообщений в файл
  Future<void> saveToFile(String userName) async {
    //ПробуЕМ
    try {
      //чистим ник
      userName = cleanFileName(userName);
      //определяем безопасную и доступную директорию для сохранения файлов приложения,
      //в Android это будет ../data/user/0/com.example.chat_bot/files/..
      //в Windows это будет ../username/documents/..
      final directory = await getApplicationDocumentsDirectory();
      final file = File('${directory.path}/$userName.json');

      //преобразование сообщений в Json понятный формат
      List<Map<String, dynamic>> messagesJson = [];
      for (var message in allMessages) {
        String messageValue = await message
            .message; // Ожидаем завершения Future из поля класса и получаем его значение
        Map<String, dynamic> messageJson = {
          'message': messageValue,
          'senderName': message.senderName,
          'sender': message.sender,
          'sentTime': message.sentTime.toIso8601String(),
        };
        messagesJson.add(messageJson);
      }
      //кодируем и записываем
      final jsonString = json.encode(messagesJson);
      file.writeAsString(jsonString);
    } catch (e) {
      //Если что, просто выводим ошибку
      print('ERROR::FAILED TO SAVE USER DATA: $e');
    }
  }

  ///Функция очистки ника пользователя от опасных символов для файловых систем OC
  String cleanFileName(String fileName) {
    final invalidChars = RegExp(r'[<>:"/\|?*]');
    return fileName.replaceAll(invalidChars, '');
  }

  ///Метод загрузки из файла в список всех сообщений
  Future<void> loadFromFile(String userName) async {
    //ПробуЕМ
    try {
      //чистим ник
      userName = cleanFileName(userName);
      //определяем безопасную и доступную директорию для сохранения файлов приложения,
      //в Android это будет ../data/user/0/com.example.chat_bot/files/..
      //в Windows это будет ../username/documents/..
      final directory = await getApplicationDocumentsDirectory();
      final file = File('${directory.path}/$userName.json');
      //ожидаем чтения из файла в переменную
      String contents = await file.readAsString();
      //декодируем, с помощью метода описанного в классе Message
      List<dynamic> messagesJson = json.decode(contents);
      allMessages = messagesJson.map((json) => Message.fromJson(json)).toList();
    } catch (e) {
      //Если что, просто выводим ошибку
      print('ERROR::FAILED TO LOAD USER DATA: $e');
    }
  }

  ///Регулярно-делигатный словарь вида <какая либо строка> ArrowFunction <что делать>
  ChatBot() {
    commands = {
      RegExp(r'^.*(привет)|(здравствуй.{0,2}).*$', caseSensitive: false): () =>
          sayHello(),
      RegExp(r'^.*(пока)|(до свидания).*$', caseSensitive: false): () =>
          sayBye(),
      RegExp(r'^.*(как.* дела)|(как жи.*)|(как ты).*$', caseSensitive: false):
          () => sayAnswerHowAreYou(),
      RegExp(r'(спасибо)|(благодарю)', caseSensitive: false): () =>
          sayYoureWelcome(),
      RegExp(r'(help)|(помоги)|(помощь)', caseSensitive: false): () =>
          sayHelp(),
      RegExp(r'^(\d+)\s*([+\-*/])\s*(\d+)$', caseSensitive: false):
          (String mathToSolve) => sayCalculationResult(mathToSolve),
      RegExp(r'.*@.*', caseSensitive: false): () => sayPochta(),
      RegExp(r'^.*(врем.{0,3})|(час.{0,3}).*$', caseSensitive: false): () =>
          sayTimeNow(),
      RegExp(r'^.*(дат.{0,1})|(сегодня).*$', caseSensitive: false): () =>
          sayDateNow(),
      RegExp(r'(погода)|(как там на улице)', caseSensitive: false): () =>
          sayWeather(),
      RegExp(r'^.*(ножницы)|(камень)|(бумага).*$', caseSensitive: false):
          (String playerChoice) => playRockPaper(playerChoice),
      RegExp(r'^.*(сколько сообщений.{0,3})|(истор.{0,3}).*$',
          caseSensitive: false): () => sayHowManyMesseges(),
    };
  }

  ///Метод обработки ботом сообщений, userInput понятно что такое, есть несколько вариаций и можно добавить больше
  ///Некоторые функции требуют дополнительного ввода от пользователя, они обрабатываются в условии command.pattern.contains(<команда, задаваемая пользователем>)
  Future<String> processCommand(String userInput) {
    //перебираем каждую регулярку
    for (var command in commands.keys) {
      //есть совпадения - делаем штуки
      if (command.hasMatch(userInput)) {
        if (commands[command] is Function) {
          if (command.pattern.contains(r'^(\d+)\s*([+\-*/])\s*(\d+)$')) {
            return commands[command]!(userInput);
          }
          if (command.pattern.contains(r'^.*(ножницы)|(камень)|(бумага).*$')) {
            return commands[command]!(userInput);
          } else {
            return commands[command]!();
          }
        }
      }
    }
    //нет - плачем
    return Future.value(
        "Я не знаю, что ответить!!! 😭 Попробуйте еще раз или воспользуйтесь командой 'help'.");
  }

  /// Метод бота для вывода общего кол-ва собщений у пользователя, зарегистрированного в данный момент
  Future<String> sayHowManyMesseges() {
    return Future.value(
        'Таааак... \nСовместными усилиями мы написали ${allMessages.length + 1} сообщений! 💪');
  }

  /// Метод бота для игры в "Камень, ножницы, бумага"
  Future<String> playRockPaper(String playerChoice) {
    List<String> choices = ["камень", "бумага", "ножницы"];

    Random random = Random();
    final int rand = random.nextInt(3);

    String computerChoice = choices[rand];
    String result = "";

    if (playerChoice.contains(computerChoice)) {
      result = "Мой выбор: $computerChoice... \nЭто ничья! 🤷";
    } else {
      switch (playerChoice) {
        case "камень":
          result = computerChoice == "ножницы"
              ? "Я выбрал $computerChoice... \nТы выиграл! ✔️"
              : "Я выбрал $computerChoice...\nТы проиграл! ❌";
          break;
        case "бумага":
          result = computerChoice == "камень"
              ? "Я выбрал $computerChoice... \nТы выиграл! ✔️"
              : "Я выбрал $computerChoice...\nТы проиграл! ❌";
          break;
        case "ножницы":
          result = computerChoice == "бумага"
              ? "Я выбрал $computerChoice... \nТы выиграл! ✔️"
              : "Я выбрал $computerChoice...\nТы проиграл! ❌";
          break;
      }
    }
    return Future.value(result);
  }

  /// Асинхронный метод бота для вывода погоды, асинхронный - т.к. ждем ответа сервера
  Future<String> sayWeather() async {
    WeatherService weatherService = WeatherService();
    String result;
    try {
      //тут можно менять города, но я не стал давать пользователю возможность сменить город, т.к. на openweathermap город должен
      //быть написан на англ языке
      result = await weatherService.getWeatherData('Chita');
    } catch (error) {
      return ("${error.toString()}\n Короче, нет соединения с интернетом!");
      //бывают и ошибки, не стисняемся показывать пользователю, т.к. там есть ошибка "Нет интернета"
    }
    return (result); // Вывод информации о погоде
  }

  /// Метод бота для help
  Future<String> sayHelp() {
    return Future.value(
        "'Привет' : Приветствие\n'Пока': Прощание\n'Как дела?': Бот тоже личность!"
        "\n'Спасибо': Смущать бота\n'Дата' или 'Сегодня': Узнать текущую дату\n'Время' или 'Который час': Узнать текущую дату или время"
        "\n'Погода' или 'Как там на улице?': Возвращает текущую погоду в Чите"
        "\n'<Число> (+-*/) <число>': Вернет результат арифметического действия"
        "\n'Камень' или 'Ножницы' или 'Бумага': для того чтобы сыграть в 'Камень, ножницы бумага!'"
        "\n'Сколько сообщений?' или 'История': покажет сколько вы сообщений написали");
  }

  /// Метод бота для пока
  Future<String> sayBye() {
    return Future.value('Пока! 🫠');
  }

  /// Метод бота для привет
  Future<String> sayHello() {
    return Future.value('Привет! Чем могу помочь?');
  }

  /// Метод бота для как дела?
  Future<String> sayAnswerHowAreYou() {
    return Future.value('У меня все БОТоотлично! 😀');
  }

  /// Метод бота для спасибо
  Future<String> sayYoureWelcome() {
    return Future.value('Пожалуйста, рад был помочь. 🤗');
  }

  /// Метод бота для определения почты (была в тесте)
  Future<String> sayPochta() {
    return Future.value(
        'Это чья-то почта! Пока что я не знаю, что с ней делать.');
  }

  /// Метод бота для вывода времени сейчас
  Future<String> sayTimeNow() {
    return Future.value("Сейчас ${DateFormat('HH:mm').format(DateTime.now())}");
  }

  /// Метод бота для вывода даты сегодня
  Future<String> sayDateNow() {
    initializeDateFormatting('ru');
    return Future.value(
        "Сегодня ${DateFormat('EEEE', 'ru').format(DateTime.now())} ${DateFormat('dd.MM.yyyy').format(DateTime.now())}");
  }

  /// Метод бота для арифметических операций +-/*
  Future<String> sayCalculationResult(String mathToSolve) {
    double result;
    RegExp regex = RegExp(r'^(\d+)\s*([+\-*/])\s*(\d+)$');
    //смотрим на совпадения с помощью match
    RegExpMatch? match = regex.firstMatch(mathToSolve);
    //разбиваем mathc на группы строк
    String? num1 = match!.group(1);
    String? operator = match.group(2);
    String? num2 = match.group(3);

    //и работаем с ними
    switch (operator) {
      case '+':
        result = double.parse(num1 ?? "") + double.parse(num2 ?? "");
        break;
      case '-':
        result = double.parse(num1 ?? "") - double.parse(num2 ?? "");
        break;
      case '*':
        result = double.parse(num1 ?? "") * double.parse(num2 ?? "");
        break;
      case '/':
        result = double.parse(num1 ?? "") / double.parse(num2 ?? "");
        break;
      default:
        result = 0;
    }
    return Future.value('Результат: $result');
  }
}
