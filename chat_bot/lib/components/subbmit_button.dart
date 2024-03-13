///Autor: Eargosha

import 'package:flutter/material.dart';

// StatelessWidget и StatefulWidget - это два основных виджета в Flutter, которые используются для построения 
// пользовательского интерфейса. Вот их основные различия:

// **StatelessWidget**:
//    - StatelessWidget является виджетом, у которого состояние не изменяется после создания.
//    - Он не имеет внутреннего состояния, и его вид перерисовывается целиком при изменении данных.
//    - Подходит для простых виджетов, у которых не требуется сохранение состояния или реакция на внешние изменения.

// **StatefulWidget**:
//    - StatefulWidget является виджетом, у которого состояние может изменяться во время работы приложения.
//    - Он состоит из двух классов: класса виджета (StatefulWidget) и класса состояния (State), который хранит изменяемое состояние виджета.
//    - Подходит для виджетов, которым требуется сохранение состояния, обновление данных или реакция на внешние события.

/// SubbmitButon, кнопка подтверждения, да много, зато свое
/// text - надпись, onPressed - функция для выполнения при нажатии на клавишу
class SubbmitButon extends StatelessWidget {
  
  final String text;
  final void Function()? onPressed;

  const SubbmitButon({
    super.key,
    required this.text,
    required this.onPressed,
  });

  @override
  Widget build(BuildContext context) {
    return TextButton(
      style: ButtonStyle(
        overlayColor:
            MaterialStateProperty.all(Colors.deepPurple.shade200),
        shape: MaterialStateProperty.all(
          RoundedRectangleBorder(
            borderRadius: BorderRadius.circular(12),
          ),
        ),
        backgroundColor: MaterialStateProperty.all(
            Colors.deepPurple.shade400),
      ),
      onPressed: onPressed,
      child: Padding(
        padding: const EdgeInsets.symmetric(horizontal: 30.0),
        child: Text(
          text,
          style: const TextStyle(
            color: Colors.white,
            fontSize: 16,
            fontWeight: FontWeight.bold,
          ),
        ),
      ),
    );
  }
}
