///Autor: Eargosha

import 'package:flutter/material.dart';

//Это просто описание темы приложения, не более
ThemeData lightMode = ThemeData(
  colorScheme: ColorScheme.light(
    background: const Color.fromARGB(255, 255, 255, 255),
    primary: Colors.grey.shade500,
    secondary: Colors.grey.shade100,
    tertiary: Colors.white,
    inversePrimary: Colors.grey.shade900,
    error: Colors.red,
  ),
);
