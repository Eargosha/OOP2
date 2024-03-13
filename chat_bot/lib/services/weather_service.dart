///Autor: Eargosha

import 'dart:convert';
import 'package:http/http.dart' as http;

/// Класс для парсинга погоды в определенном городишке, город писать Латиницей
class WeatherService {
  final String apiKey = '6e8f57a9bb1328548a237669006646b7';

  /// Ассинхронная функция, поэтому используем переменную в будущем, async как ключ. слово и await, там, где нужно ждать ответа
  Future<String> getWeatherData(String city) async {
    final String baseUrl = 'https://api.openweathermap.org/data/2.5/weather?q=+$city+&appid=';
    final response = await http.get(Uri.parse(baseUrl + apiKey));
    //если код == 200, что есть код без ошибок для openweathermap
    if (response.statusCode == 200) {
      //то декодируем тело полученного ответа с помощью Json и записываем в словарь data
      Map<String, dynamic> data = jsonDecode(response.body);
      //далее вытаскиваем значения из словаря
      double temperature = data['main']['temp'] - 273;
      double feelings = data['main']['feels_like'] - 273;
      int humidity = data['main']['humidity'];
      //создаем строку вывода
      String weatherInfo =  'Итак в Чите:\nТемпература: ${temperature.toStringAsFixed(2)} °C\nЧувствуется как ${feelings.toStringAsFixed(2)} °C\nВлажность $humidity %';
      return weatherInfo;
    } else {
      //если что кидаем из окна ошибку
      throw Exception('Failed to load weather data');
    }
  }
}