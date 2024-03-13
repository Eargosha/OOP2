///Autor: Eargosha

import 'package:chat_bot/themes/light_mode.dart';
import 'package:flutter/material.dart';
import 'pages/login_page.dart';

///Главная функция Dart, она запускает приложение
void main() {
  runApp(const MainApp());
}

///MainApp - оболочка для всех будущих страниц в приложении
class MainApp extends StatelessWidget {
  const MainApp({super.key});

  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      //показываем пользователю LoginPage
      home: const LoginPage(),
      theme: lightMode,
    );
  }
}
