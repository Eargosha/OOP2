///Autor: Eargosha

import 'package:flutter/material.dart';

/// TextFieldInput используется для ввода даных, да есть базовый TextField, но он мне НЕ нравится
/// он требует:
/// hint - подсказку для ввода
/// controller - обьект класса TextEditingController, нужно для того, чтобы считывать данные введенные в TextInput
/// onPresShiftAndEnter - функция, что будет выполняться при нажатии на Shift+Enter, тогда, когда TextInput выделен пользователем
/// maxLines - кол-во линий для ввода в TextInput
class TextFieldInput extends StatelessWidget {
  final String hint;
  final TextEditingController controller;
  final void Function() onPresShiftAndEnter;
  final int maxLines;

  const TextFieldInput({
    super.key,
    required this.hint,
    required this.controller,
    required this.onPresShiftAndEnter,
    required this.maxLines,
  });

  @override
  Widget build(BuildContext context) {
    //преременная FocusNode, нужна для того, чтобы обрабатывать нажатия клавиш shift + enter
    var focusNode = FocusNode(canRequestFocus: true, onKey: (node, event) {
      if (event.isShiftPressed && event.logicalKey.keyLabel == 'Enter') {
        onPresShiftAndEnter();
        return KeyEventResult.handled;
      }
      return KeyEventResult.ignored;
    });
    //Да, построен на базовом TextField
    return TextField(
      maxLines: maxLines,
      autofocus: true,
      focusNode: focusNode,
      controller: controller,
      textAlign: TextAlign.left,
      decoration: InputDecoration(
        enabledBorder: OutlineInputBorder(
          borderSide: BorderSide(color: Theme.of(context).colorScheme.tertiary),
        ),
        focusedBorder: OutlineInputBorder(
          borderSide: BorderSide(
            color: Theme.of(context).colorScheme.primary,
          ),
        ),
        border: OutlineInputBorder(
          borderRadius: BorderRadius.circular(16),
          borderSide: const BorderSide(
            width: 0,
            style: BorderStyle.none,
          ),
        ),
        hintText: hint,
        hintStyle: TextStyle(color: Theme.of(context).colorScheme.primary),
        contentPadding: const EdgeInsets.only(top: 10.0, left: 16),
        filled: true,
        fillColor: Theme.of(context).colorScheme.secondary,
      ),
    );
  }
}
