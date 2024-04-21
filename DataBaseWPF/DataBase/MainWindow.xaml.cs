using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;

namespace DataBase
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        /// создаем сылки на обьекты классов окон и моделей
        DataB listOfBooks;
        AddBookWindow addBookWindow;
        EditBookWindow editBookWindow;
        FindBookWindow findBookWindow;
        TextBlock statusBarText;
        Timer saveTimer;
        int callCounter = 1;


        public MainWindow()
        {
            InitializeComponent();
            // listOfBooks.data - тот список, который показывается пользователю
            listOfBooks = new DataB();
            listOfBooks.data = new ObservableCollection<Book>(listOfBooks.Books.ToList());
            // указываем источник данных для dataGrid
            dataGrid.ItemsSource = listOfBooks.data;


            // находим имя TextBlock в StatusBar
            statusBarText = statusBar.FindName("textBlockStatus") as TextBlock;


            // создаем таймер с интервалом 3 минуты (180000 миллисекунд)
            saveTimer = new Timer(180000);
            // передаем в его функцию автосохранения
            saveTimer.Elapsed += AutoSaveData;
            // ресетаем его и снова запускаем
            saveTimer.AutoReset = true;
            saveTimer.Enabled = true;


            // добавляю горячие клавиши
            shortCuts();
        }

        //Функцкия для создания горячих клавиш
        void shortCuts()
        {
            // Alt+A = создать запись
            // Создаем команду и привязываем к ней горячую клавишу для Alt+A
            RoutedCommand addRecordShortCut = new RoutedCommand();
            CommandBinding bindingCtrlA = new CommandBinding(addRecordShortCut);
            bindingCtrlA.Executed += btnAdd_Click;
            this.CommandBindings.Add(bindingCtrlA);

            // Создаем клавиатурное событие и привязываем к нему горячую клавишу Alt+A
            KeyGesture keyGestureCtrlA = new KeyGesture(Key.A, ModifierKeys.Alt);
            InputBinding inputBindingCtrlA = new InputBinding(addRecordShortCut, keyGestureCtrlA);
            this.InputBindings.Add(inputBindingCtrlA);

            // Ctrl+D = удалить запись
            // Создаем команду и привязываем к ней горячую клавишу для Ctrl+D
            RoutedCommand deleteRecordShortCut = new RoutedCommand();
            CommandBinding bindingCtrlD = new CommandBinding(deleteRecordShortCut);
            bindingCtrlD.Executed += btnRemove_Click; // Обработчик события для Ctrl+D
            this.CommandBindings.Add(bindingCtrlD);

            // Создаем клавиатурное событие и привязываем к нему горячую клавишу Ctrl+D
            KeyGesture keyGestureCtrlD = new KeyGesture(Key.D, ModifierKeys.Control);
            InputBinding inputBindingCtrlD = new InputBinding(deleteRecordShortCut, keyGestureCtrlD);
            this.InputBindings.Add(inputBindingCtrlD);

            // Ctrl+E = изменить запись
            // Создаем команду и привязываем к ней горячую клавишу для Ctrl+E
            RoutedCommand editRecordShortCut = new RoutedCommand();
            CommandBinding bindingCtrlE = new CommandBinding(editRecordShortCut);
            bindingCtrlE.Executed += btnEdit_Click; // Обработчик события для Ctrl+E
            this.CommandBindings.Add(bindingCtrlE);

            // Создаем клавиатурное событие и привязываем к нему горячую клавишу Ctrl+E
            KeyGesture keyGestureCtrlE = new KeyGesture(Key.E, ModifierKeys.Control);
            InputBinding inputBindingCtrlE = new InputBinding(editRecordShortCut, keyGestureCtrlE);
            this.InputBindings.Add(inputBindingCtrlE);

            // Alt+Ctrl+C = почистить таблицу
            // Создаем команду и привязываем к ней горячую клавишу для Alt+Ctrl+C
            RoutedCommand clearTableShortCut = new RoutedCommand();
            CommandBinding bindingAltCtrlC = new CommandBinding(clearTableShortCut);
            bindingAltCtrlC.Executed += btnClear_Click; // Обработчик события
            this.CommandBindings.Add(bindingAltCtrlC);

            // Создаем клавиатурное событие и привязываем к нему горячую клавишу Alt+Ctrl+C
            KeyGesture keyGestureAltCtrlC = new KeyGesture(Key.C, ModifierKeys.Control | ModifierKeys.Alt);
            InputBinding inputBindingAltCtrlC = new InputBinding(clearTableShortCut, keyGestureAltCtrlC);
            this.InputBindings.Add(inputBindingAltCtrlC);

            // Ctrl+F = изменить запись
            // Создаем команду и привязываем к ней горячую клавишу для Ctrl+F
            RoutedCommand findRecordShortCut = new RoutedCommand();
            CommandBinding bindingCtrlF = new CommandBinding(findRecordShortCut);
            bindingCtrlF.Executed += btnFind_Click; // Обработчик события для Ctrl+F
            this.CommandBindings.Add(bindingCtrlF);

            // Создаем клавиатурное событие и привязываем к нему горячую клавишу Ctrl+F
            KeyGesture keyGestureCtrlF = new KeyGesture(Key.F, ModifierKeys.Control);
            InputBinding inputBindingCtrlF = new InputBinding(findRecordShortCut, keyGestureCtrlF);
            this.InputBindings.Add(inputBindingCtrlF);

            // Ctrl+S = сохранить как
            // Создаем команду и привязываем к ней горячую клавишу для Ctrl+S
            RoutedCommand saveDBShortCut = new RoutedCommand();
            CommandBinding bindingCtrlS = new CommandBinding(saveDBShortCut);
            bindingCtrlS.Executed += menuItemFileSave_Click; // Обработчик события для Ctrl+S
            this.CommandBindings.Add(bindingCtrlS);

            // Создаем клавиатурное событие и привязываем к нему горячую клавишу Ctrl+S
            KeyGesture keyGestureCtrlS = new KeyGesture(Key.S, ModifierKeys.Control);
            InputBinding inputBindingCtrlS = new InputBinding(saveDBShortCut, keyGestureCtrlS);
            this.InputBindings.Add(inputBindingCtrlS);

            // Ctrl+O = открыть как
            // Создаем команду и привязываем к ней горячую клавишу для Ctrl+S
            RoutedCommand openDBShortCut = new RoutedCommand();
            CommandBinding bindingCtrlO = new CommandBinding(openDBShortCut);
            bindingCtrlO.Executed += menuItemFileOpen_Click; // Обработчик события для Ctrl+S
            this.CommandBindings.Add(bindingCtrlO);

            // Создаем клавиатурное событие и привязываем к нему горячую клавишу Ctrl+S
            KeyGesture keyGestureCtrlO = new KeyGesture(Key.O, ModifierKeys.Control);
            InputBinding inputBindingCtrlO = new InputBinding(openDBShortCut, keyGestureCtrlO);
            this.InputBindings.Add(inputBindingCtrlO);
        }

        // Функция для автосохранения
        private void AutoSaveData(object sender, ElapsedEventArgs e)
        {
            string filePath = $"BooksData auto save {callCounter}.db";
            listOfBooks.saveToFile(filePath);
            callCounter++;
            if (callCounter == 4)
            {
                callCounter = 1;
            }
        }


        /// <summary>
        /// Функция, что помогает производить вывод в statusBar, 
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="displayTimeMilliseconds">Кол-во милисекунд, через которое сообщение вернет базовое значение "В ожидании действий пользователя"</param>
        public async void DisplayAboutSystemMessage(string message, int displayTimeMilliseconds)
        {
            statusBarText.Text = message;
            await Task.Delay(displayTimeMilliseconds);
            statusBarText.Text = "В ожидании действий пользователя 🤷";
        }

        /// <summary>
        /// Обработчик события на нажатие добавления записи
        /// </summary>
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            DisplayAboutSystemMessage("Окно добавления записи было открыто 📖", 3000);
            addBookWindow = new AddBookWindow();
            addBookWindow.Owner = this;
            addBookWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            bool? result = addBookWindow.ShowDialog();

            if (result == true)
            {
                listOfBooks.add_book(addBookWindow.name, addBookWindow.autor, addBookWindow.genre, addBookWindow.depositPrice, addBookWindow.rentalPrice, addBookWindow.status, listOfBooks.data.Count());
            }
         
            DisplayAboutSystemMessage("Добавлена новая запись ➕", 3000);
        }

        /// <summary>
        /// Обработчик события на нажатие удаления записи
        /// </summary>
        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            var selectedBooks = dataGrid.SelectedItems.Cast<Book>().ToList();

            if (selectedBooks.Any())
            {
                try
                {
                    foreach (var selectedBook in selectedBooks)
                    {
                        // Вызываете метод удаления из вашего сервисного слоя или слоя доступа к данным
                        listOfBooks.remove_book(selectedBook.id);
                    }

                    // Обновляем индексы оставшихся книг в списке (если это необходимо)
                    for (int i = 0; i < listOfBooks.data.Count; i++)
                    {
                        listOfBooks.data[i].id = i + 1;
                    }

                    DisplayAboutSystemMessage("Выбранные записи были удалены ✂️", 3000);
                }
                catch
                {
                    MessageBox.Show(mainWindow, "Некорректный идентификатор элемента!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    DisplayAboutSystemMessage("Ошибка: некорректный идентификатор элемента! ❌", 3000);
                }

                // Обновляем DataGrid, чтобы отразить изменения
                dataGrid.Items.Refresh();
            }
            else
            {
                MessageBox.Show(mainWindow, "Выберите элементы из таблицы для удаления, кликнув по ним", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик события на нажатие кнопки редактирования записи
        /// </summary>
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            // Получаем выбранную книгу из DataGrid
            var selectedBook = dataGrid.SelectedItem as Book;

            if (selectedBook != null)
            {
                DisplayAboutSystemMessage("Окно редактирования записи было открыто 📖", 3000);
                // Открываем окно редактирования
                editBookWindow = new EditBookWindow();
                editBookWindow.Owner = this;
                editBookWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                editBookWindow.ShowDialog();

                // Находим книгу для редактирования в списке
                Book foundBook = listOfBooks.findWithId(selectedBook.id);

                if (foundBook != null)
                {
                    // Производим изменения, если в окне редактирования были внесены данные
                    listOfBooks.data[selectedBook.id - 1].Name = string.IsNullOrEmpty(editBookWindow.name) ? listOfBooks.data[selectedBook.id - 1].Name : editBookWindow.name;
                    listOfBooks.data[selectedBook.id - 1].Autor = string.IsNullOrEmpty(editBookWindow.autor) ? listOfBooks.data[selectedBook.id - 1].Autor : editBookWindow.autor;
                    listOfBooks.data[selectedBook.id - 1].Genre = string.IsNullOrEmpty(editBookWindow.genre) ? listOfBooks.data[selectedBook.id - 1].Genre : editBookWindow.genre;
                    listOfBooks.data[selectedBook.id - 1].DepositPrice = string.IsNullOrEmpty(editBookWindow.depositPrice) ? listOfBooks.data[selectedBook.id - 1].DepositPrice : editBookWindow.depositPrice;
                    listOfBooks.data[selectedBook.id - 1].RentalPrice = string.IsNullOrEmpty(editBookWindow.rentalPrice) ? listOfBooks.data[selectedBook.id - 1].RentalPrice : editBookWindow.rentalPrice;
                    listOfBooks.data[selectedBook.id - 1].Status = string.IsNullOrEmpty(editBookWindow.status) ? listOfBooks.data[selectedBook.id - 1].Status : editBookWindow.status;

                    DisplayAboutSystemMessage("Выбранная запись была изменена 📝", 3000);
                }
                else
                {
                    // Если книга не найдена, возможно, произошла ошибка, и вы можете обработать это здесь
                    MessageBox.Show(mainWindow, "Невозможно редактировать книгу", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dataGrid.Items.Refresh();
            }
            else
            {
                // Если ничего не выбрано в DataGrid, можно показать сообщение об ошибке или просто ничего не делать
                MessageBox.Show(mainWindow, "Выберите элемент из таблицы для редактирования, кликнув по нему", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            // Обновляем источник данных DataGrid, чтобы отразить изменения
            dataGrid.Items.Refresh();
        }

        /// <summary>
        /// Обработчик события кнопки "О приложении"
        /// </summary>
        private void menuItemAboutApp_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "База даных 'Книга' Ver 1.0\n\nФунции:\n-Добавления записи\n-Удаления записи\n-Редактирования записи\n-Сохранения записей\n-Чтения записей\n-Поиска записей", "О приложении", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Обработчик события кнопки "О разработчике"
        /// </summary>
        private void menuItemAboutDev_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(this, "Автор: Eargosha\nПредложения: @eargosha", "О разработчике", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// Обработчик события саохранить как
        /// </summary>
        private void menuItemFileSave_Click(object sender, RoutedEventArgs e)
        {
            // Создаем диалоговое окно сохранения файла
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            // Фильтруем файлы
            saveFileDialog.Filter = "Файлы data base (*.db)|*.db|Все файлы (*.*)|*.*";
            saveFileDialog.FilterIndex = 1;
            // Задаем начальное имя файла
            saveFileDialog.FileName = "BooksData " + DateTime.Now.ToString("yyyy-MM-dd HH-mm");

            // Открываем диалоговое окно сохранения файла и проверяем, нажата ли кнопка "OK"
            if (saveFileDialog.ShowDialog() == true)
            {
                // Получаем путь к выбранному файлу
                string filePath = saveFileDialog.FileName;

                // Сохраняем данные в выбранный файл
                listOfBooks.saveToFile(filePath);
                DisplayAboutSystemMessage("Вся информация сохранена в файл " + saveFileDialog.FileName + " ✔️", 3000);
            }
        }

        /// <summary>
        /// Обработчик события открыть как
        /// </summary>
        private void menuItemFileOpen_Click(object sender, RoutedEventArgs e)
        {
            // Создаем диалоговое окно открытия файла
            OpenFileDialog openFileDialog = new OpenFileDialog();
            // Фильтруем файлы
            openFileDialog.Filter = "Файлы data base (*.db)|*.db|Все файлы (*.*)|*.*";
            openFileDialog.FilterIndex = 1;

            // Открываем диалоговое окно открытия файла и проверяем, нажата ли кнопка "OK"
            if (openFileDialog.ShowDialog() == true)
            {
                // Получаем путь к выбранному файлу
                string filePath = openFileDialog.FileName;

                // Загружаем данные из выбранного файла
                listOfBooks.loadFromFile(filePath);

                // Обновляем источник данных DataGrid, чтобы отобразить загруженные данные
                dataGrid.ItemsSource = listOfBooks.data;
                DisplayAboutSystemMessage("Вся информация загружена из файла " + filePath + " 📚", 3000);
            }
        }

        /// <summary>
        /// Обработчик события кнопки очищения таблицы, чистит сам list, где хранятся данные для вывода пользователя
        /// </summary>
        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            listOfBooks.data.Clear();
            dataGrid.ItemsSource = listOfBooks.data;
            DisplayAboutSystemMessage("Таблица была очищена 🧹", 3000);
        }

        /// <summary>
        /// Обработчик события кнопки поиска элемента
        /// </summary>
        private void btnFind_Click(object sender, RoutedEventArgs e)
        {
            DisplayAboutSystemMessage("Окно поиска было открыто 📎", 3000);
            findBookWindow = new FindBookWindow(listOfBooks.data);
            findBookWindow.Owner = this;
            findBookWindow.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            findBookWindow.ShowDialog();
        }
    }
}
