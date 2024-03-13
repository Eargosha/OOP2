 ///Autor: Eargosha
// ignore_for_file: must_be_immutable

import 'package:chat_bot/components/chat_buble.dart';
import 'package:chat_bot/models/message.dart';
import 'package:flutter/material.dart';
import '../components/text_field_input.dart';
import '../models/chat_bot.dart';

//Страница, где реализован чат с ботом, требует для своего создания username - имя пользователя, что получаем от LoginPage в данном приложении
class ChatPage extends StatefulWidget {
  String userName;

  ChatPage({
    super.key,
    required this.userName,
  });

  @override
  State<ChatPage> createState() => _ChatPageState();
}

//У этой страницы есть свое состояние, необходимо для динамических отображений chat_bubble
class _ChatPageState extends State<ChatPage> {
  //инициализация бота
  String botName = "Bib Botik";
  ChatBot bibBot = ChatBot();
  //даешь геттер для UN
  String get userName => widget.userName;
  //определяем работу контроллера для получения сообщений от пользователя
  final TextEditingController _messageController = TextEditingController();

  ///Метод loading, ассинхроннй метод, необходим для загрузки сообщений на страницу по определенному имени пользователя
  loading() async {
    await bibBot.loadFromFile(userName);
    // не стал добавлять это приветсвенное сообщение в самого бота, хотя можно было
    if (bibBot.allMessages.isEmpty) {
      bibBot.allMessages.add(Message(
          Future.value(
              'Привет, $userName! Ты впервые запустил этот чат, напиши "help" или "помощь", чтобы узнать его возмонжости'),
          botName,
          false,
          DateTime.now()));
    }
    //вызываем setState и пустой, т.к. он указывает initState, где нужно произвести обновление виджета
    setState(() {});
    refreshChat();
  }

  /// initState - эта штука есть у всех виджетов, так что перезапись.
  /// нужен для обновления чата с ботом, когда:
  /// 1 загружаем соо из файла
  /// 2 пользователь пишет сообщение
  /// 3 ну и задает имя боту, строчка просто прижилась тут
  @override
  void initState() {
    super.initState();
    loading();
    bibBot.botName = botName;
  }

  /// Метод отправки сообщений со страницы боту и тут же сохраняем в файл
  void sendMessage() {
    setState(() {
      bibBot.sendMessage(_messageController.text, userName);
      _messageController.clear();
      bibBot.saveToFile(userName);
    });
    refreshChat();
  }

  /// Функция для обновления, чтобы чат двигался за новым сообщением в ListView.builder
  void refreshChat() {
    //Концепция работы метода addPostFrameCallback заключается в том, что он позволяет выполнить определенный код после
    //завершения построения виджетов (layout) на экране.
    //Когда новый chatBubble добавляется в список сообщений, Flutter должен сначала перестроить интерфейс,
    //чтобы отобразить этот новый элемент. После того, как перестроение завершено,
    //метод addPostFrameCallback вызывает переданную ему функцию
    WidgetsBinding.instance.addPostFrameCallback((_) {
      _scrollController.animateTo(
        _scrollController.position.maxScrollExtent,
        duration: Duration(milliseconds: 300),
        curve: Curves.easeOut,
      );
    });
  }

  ScrollController _scrollController = ScrollController();

  @override
  Widget build(BuildContext context) {
    //Scaffold есть пространство выделенного экрана
    return Scaffold(
      //Appbar есть AppBar
      appBar: AppBar(
        centerTitle: true,
        title: Text(
          botName,
          textAlign: TextAlign.center,
          style: const TextStyle(fontWeight: FontWeight.bold, fontSize: 24),
        ),
        backgroundColor: const Color.fromARGB(255, 215, 223, 231),
        elevation: 200,
      ),
      //в виде колонки показываем каждое сообщение, что содержится внутри списка всех сообщений
      body: Column(
        children: [
          // благодаря Expanded можем оттолкнуть список из chatbuble от buildUserInput
          Expanded(
            // благодаря ListView.builder можем строить списки из chatbuble и прокручивать сообщения вверх и вниз
            child: ListView.builder(
              controller: _scrollController,
              itemCount: bibBot.allMessages.length,
              itemBuilder: (context, index) {
                return ChatBubble(
                  bubbleInner: bibBot.allMessages[index],
                );
              },
            ),
          ),
          //Виджет для места, где происходит ввод сообщений
          buildUserInput(),
        ],
      ),
    );
  }

  // Виджет ввода сообщений для user
  Widget buildUserInput() {
    return Container(
      decoration: const BoxDecoration(
        borderRadius: BorderRadius.only(
            topLeft: Radius.circular(8), topRight: Radius.circular(8)),
        color: Color.fromARGB(255, 215, 223, 231),
      ),
      child: Padding(
        padding: const EdgeInsets.all(8.0),
        child: Row(
          children: [
            // Место для ввода
            Expanded(
              child: Padding(
                padding: const EdgeInsets.only(left: 8.0),
                child: TextFieldInput(
                  maxLines: 2,
                  controller: _messageController,
                  hint: "Введите сообщение",
                  onPresShiftAndEnter: sendMessage,
                ),
              ),
            ),
            // Кнопка отправки
            Padding(
              padding: const EdgeInsets.only(left: 6.0),
              child: IconButton(
                onPressed: sendMessage,
                icon: const Icon(Icons.send),
              ),
            ),
          ],
        ),
      ),
    );
  }
}
