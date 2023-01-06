# &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;NIX Solutions Module #3 Homework #7

                                                          Задача:

-Створити Logger з логікою асинхронного запису до файлу.\
-Logger містить подію, яка повідомляє про те, коли необхідно робити резервну копію файлу логів.\
-На події Logger’a підписаний клас Starter\
-Резервну копію (Backup) - робити після кожного N запису в лога.

-Резервні копії це\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;○ файл з ім'ям у вигляді часу створення файлу\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;○ кожен новий файл містить на N записів більше\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;○ усі файли копій лежать у папці Backup\
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;○ N - це число, що конфігурується з файлу

-Запустити 2 асинхронні методи, кожен з яких зробить по 50 записів у логи
