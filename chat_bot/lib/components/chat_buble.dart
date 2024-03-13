///Autor: Eargosha
import 'package:chat_bot/models/message.dart';
import 'package:flutter/material.dart';
import 'package:intl/intl.dart';

/// Класс интерфейса - пузырек сообщения, требует себе поле bubbleInner, в котором должен содержаться обьект класса Message
class ChatBubble extends StatefulWidget {
  final Message bubbleInner;

  const ChatBubble({
    super.key,
    required this.bubbleInner,
  });

  @override
  State<ChatBubble> createState() => _ChatBubbleState();
}
//Используем состояние State т.к. внутри класса происхоодят динамические изменения в виджетах
//при помощи BuildContext
class _ChatBubbleState extends State<ChatBubble> {
  @override
  Widget build(BuildContext context) {
    return Container(
      padding: const EdgeInsets.all(16),
      margin: widget.bubbleInner.sender
          ? const EdgeInsets.only(top: 5, bottom: 5, left: 50)
          : const EdgeInsets.only(top: 5, bottom: 5, right: 50),
      decoration: BoxDecoration(
          color: widget.bubbleInner.sender
              ? const Color.fromRGBO(119, 83, 236, 1)
              : const Color.fromARGB(255, 196, 196, 196),
          borderRadius: widget.bubbleInner.sender
              ? const BorderRadius.only(
                  topLeft: Radius.circular(35), bottomLeft: Radius.circular(35))
              : const BorderRadius.only(
                  topRight: Radius.circular(35),
                  bottomRight: Radius.circular(35))),
      child: Column(
        mainAxisAlignment: MainAxisAlignment.start,
        crossAxisAlignment: CrossAxisAlignment.start,
        children: [
          Row(
            mainAxisAlignment: MainAxisAlignment.spaceBetween,
            children: [
              // Показываем имя отправителя сообщения, Expanded позволяет показывать ... если текст не влазит в экран
              Expanded(
                child: Text(
                  widget.bubbleInner.senderName,
                  style: TextStyle(
                    color:
                        widget.bubbleInner.sender ? Colors.white : Colors.black,
                    fontWeight: FontWeight.bold,
                    fontSize: 18,
                  ),
                  maxLines: 1,
                  overflow: TextOverflow.ellipsis,
                ),
              ),
              // Показываем время отправки, Expanded позволяет показывать ... если текст не влазит в экран
              Expanded(
                child: Text(
                  DateFormat('dd.MM.yy аt HH:mm').format(widget.bubbleInner.sentTime),
                  maxLines: 1,
                  overflow: TextOverflow.ellipsis,
                  textAlign: TextAlign.right,
                  style: TextStyle(
                    color: widget.bubbleInner.sender
                        ? Colors.white70
                        : const Color.fromARGB(176, 33, 33, 33),
                  ),
                ),
              ),
            ],
          ),
          // Показываем содержимое сообщения, используем Flex, чтобы сообщение могло переходить на новые строки
          Flex(
            direction: Axis.vertical,
            children: [
              // Используем FutureBuilder<String> т.к. бот возращает значения переменных "будущего"
              FutureBuilder<String>(
                future: widget.bubbleInner.message,  //что должно лежать в сообщении
                builder: (context, snapshot) {       //в зависимости от ситуации, работаем с snapshot'ом
                  if (snapshot.connectionState == ConnectionState.waiting) {
                    return const CircularProgressIndicator(); // Показываем индикатор загрузки, пока данные загружаются
                  } else {                                    // Или возращаем текст сообщения, как загрузилось
                    return Text(
                      snapshot.data ?? '',        //Иногда Future может вернуть пустоту
                      style: TextStyle(
                        color: widget.bubbleInner.sender
                            ? Colors.white
                            : Colors.grey[900],
                        fontSize: 16,
                      ),
                    );
                  }
                },
              ),
            ],
          ),
        ],
      ),
    );
  }
}
