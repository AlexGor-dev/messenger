# messenger
Мессенджер для ton blockchain.

*********************************************************
Файлы fift, func, lite-client - собраны на Ubuntu 18.04.3.
Необходимо установить Mono - "deb-packages.sh".
Программа запускается командой - "mono Messenger.exe" из папки bin\.
Видео по запуску " Messenger.mp4".

*********************************************************

Предназначен для обмена шифрованными сообщениями, между пользователями.
Используется "Асимметричное шифрование".
Отправитель шифрует своё сообщение публичным ключом получателя.
Получатель расшифровывает сообщение своим приватным ключом.
Ключи используются те же, что и для подписи, и проверки подписи сообщений в коде контракта, сгенерированные при создании контракта (Ed25519).
Смарт контракты " Messenger " бывают двух типов - публичный и приватный.
- Публичный контракт принимает сообщения от всех пользователей, не добавленных в чёрный список.
- Приватный контракт принимает сообщения только от пользователей из своего списка (добавленных).

С помощью смарт контракта " Messenger " можно:
- Обмениваться шифрованными сообщениями с другими владельцами такого же контракта.
- Одновременно обмениваться сообщениями с несколькими пользователями.
- Добавлять / удалять пользователей.
- Добавлять / удалять пользователей в чёрный список.
- Отправлять граммы собеседнику.
- Видеть изменения баланса.
- Изменять состояние (online, offline).
- Удалять сообщения из ton blockchain (3 варианта).
- 1. Удалить одно сообщение по ID.
- 2. Удалить все сообщения у которых ID меньше либо равен указанному.
- 3. Удалить все сообщения.
На уровне кода контракта реализованы все три режима.
На уровне клиента реализован только 3 вариант.

*********************************************************

Менеджер контрактов.

На ПК храниться информация только о менеджере смарт-контрактов.
Информация о созданных в нём контрактах - хранится в смарт-контракте (Manager) в ton blockchain .
Храниться приватный ключ и адрес контракта.
Приватный ключ шифруется и дешифруется приватным ключом менеджера.
В менеджере можно:
- Создавать дочерние смарт-контракты разных типов. (на данный момент реализован один тип - "Messenger")
- Отправлять граммы дочерним контрактам.
- Изменять имя менеджера и дочерних контрактов.
- Отправлять запрос на изменение кода.
- Удалять дочерние контракты.

*********************************************************


Messenger for ton blockchain.

*********************************************************

Files fift, func, lite-client - compiled on Ubuntu 18.04.3.
You must install Mono - "deb-packages.sh".
The program is launched by the command - "mono Messenger.exe" from the bin \ folder.
Video to launch "Messenger.mp4".


*********************************************************

Designed for exchanging encrypted messages between users.
Uses Asymmetric Encryption.
The sender encrypts his message with the public key of the recipient.
The recipient decrypts the message with his private key.
The keys are used the same as for signing and verifying the signature of messages in the contract code generated when creating the contract (Ed25519).
Smart contracts "Messenger" are of two types - public and private.
- The public contract accepts messages from all users not added to the blacklist.
- A private contract accepts messages only from users from its list (added).

Using the smart contract "Messenger" you can:
- Exchange encrypted messages with other owners of the same contract.
- Simultaneously exchange messages with multiple users.
- Add / remove users.
- Add / remove users to the blacklist.
- Send grams to the other party.
- See balance changes.
- Change state (online, offline).
- Delete messages from ton blockchain (3 options).
- 1. Delete one message by ID.
- 2. Delete all messages whose ID is less than or equal to the specified.
- 3. Delete all messages.
At the contract code level, all three modes are implemented.
At the client level, only 3 options are implemented.


Contract manager.

Only information about the smart contract manager is stored on the PC.
Information about the contracts created in it is stored in the smart contract (Manager) in the ton blockchain.
The private key and address of the contract are stored.
The private key is encrypted and decrypted by the manager’s private key.
In the manager you can:
- Create subsidiary smart contracts of various types. (currently implemented one type - "Messenger")
- Send grams to child contracts.
- Change the name of the manager and child smart contracts.
- Send a request to change the code.
- Delete subsidiary contracts.


