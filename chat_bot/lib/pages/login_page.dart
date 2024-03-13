///Autor: Eargosha

import 'package:chat_bot/components/subbmit_button.dart';
import 'package:chat_bot/pages/chat_page.dart';
import 'package:flutter/material.dart';

import '../components/text_field_input.dart';

/// Страница логина для чат бота
class LoginPage extends StatefulWidget {
  const LoginPage({super.key});

  @override
  State<LoginPage> createState() => _LoginPageState();
}

class _LoginPageState extends State<LoginPage> {
  //тот самый TextEditingController, на который указывалось в Text_field_iput
  //он хранит в себе множество полезных штук, но нам нужен .text
  final _controller = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Container(
        decoration: const BoxDecoration(
          gradient: LinearGradient(
            begin: Alignment.topLeft,
            end: Alignment(0.8, 1),
            tileMode: TileMode.clamp,
            colors: <Color>[
              Color(0xff1f005c),
              Color(0xff5b0060),
              Color(0xff870160),
              Color(0xffac255e),
              Color(0xffca485c),
              Color(0xffe16b5c),
              Color(0xfff39060),
              Color(0xffffb56b),
            ],
          ),
        ),
        child: Column(
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
            Padding(
              padding: const EdgeInsets.only(bottom: 8.0),
              child: Text(
                'Ваш Чат Бот 🤖',
                style: TextStyle(
                  fontSize: 40,
                  fontWeight: FontWeight.bold,
                  color: Theme.of(context).colorScheme.background,
                ),
              ),
            ),
            Padding(
              padding: const EdgeInsets.symmetric(horizontal: 20),
              child: Container(
                decoration: BoxDecoration(
                  color: Theme.of(context).colorScheme.background,
                  borderRadius: BorderRadius.circular(12),
                ),
                child: Column(
                  children: [
                    const Padding(
                      padding: EdgeInsets.only(
                        top: 24.0,
                      ),
                      child: Text(
                        'Привет, 👋',
                        style: TextStyle(
                          fontSize: 36,
                          fontWeight: FontWeight.bold,
                        ),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.symmetric(
                          horizontal: 8.0, vertical: 4.0),
                      child: Text(
                        'Введите Ваш ник пользователя, чтобы продолжить',
                        textAlign: TextAlign.center,
                        style: TextStyle(
                          fontSize: 14,
                          color: Colors.grey[800],
                          fontWeight: FontWeight.w600,
                        ),
                      ),
                    ),
                    Padding(
                      padding: const EdgeInsets.symmetric(
                          horizontal: 34.0, vertical: 16.0),
                      child: TextFieldInput(
                        maxLines: 1,
                        hint: "Введите ваш ник...",
                        controller: _controller,
                        onPresShiftAndEnter: () { },
                      ),
                    ),
                    //Считаю, что самое главное находится тут. 
                    Padding(
                      padding: const EdgeInsets.only(bottom: 26.0),
                      //Кнопка подтверждения
                      child: SubbmitButon(
                        text: 'Начать общение с Bib Бот!',
                        onPressed: () {
                          //если не ввели имя, то бьем пользователя по головушке
                          if (_controller.text == "") {
                            showDialog(
                                context: context,
                                builder: (context) {
                                  return const AlertDialog(
                                    title: Text('Ошибка!'),
                                    content: Text("Введите ник пользователя!"),
                                  );
                                });
                          } else {
                            //иначе используем Navigator.push
                            //Navigator.push в Flutter используется для перехода на новый экран или страницу в приложении. 
                            //Он принимает контекст BuildContext и маршрут Route, который представляет собой новую страницу для отображения.
                            //Когда вызывается Navigator.push, текущий экран помещается на стек навигации, а новый экран добавляется поверх него. 
                            //Пользователь видит новый экран, который заменяет текущий, и может взаимодействовать с ним. 
                            //Когда пользователь закрывает новый экран (например, нажимает кнопку "назад"), он удаляется из стека навигации, и предыдущий экран становится активным.
                            Navigator.push(
                              context,
                              MaterialPageRoute(
                                builder: (context) => ChatPage(
                                  userName: _controller.text,
                                ),
                              ),
                            );
                          }
                        },
                      ),
                    ),
                  ],
                ),
              ),
            )
          ],
        ),
      ),
    );
  }
}
