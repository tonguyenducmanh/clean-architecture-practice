# MISA.WEB08.AMIS

## Ứng dụng được chia làm 3 phần theo nguyên tắc thiết kế clean architecture

## MISA.WEB08.AMIS.API

Là nơi chứa controller, xử lý các router chuẩn RESTful.

## MISA.WEB08.AMIS.CORE

Là lõi của ứng dụng, nơi chứa interface, services, enum, exception, ...
Chuyên chịu trách nhiệm xử lý trung tâm, xử lý dữ liệu đầu vào đầu ra,
xử lý validate dữ liệu trước khi chuyển tới cơ sở hạ tầng.

## MISA.WEB08.AMIS.INFRASTRUCTURE

Là cơ sở hạ tầng, kết nối tới các framework bên ngoài,...
Là nơi chứa các phương thức như GET, POST, PUT, DELETE để xử lý dữ liệu gửi và nhận tới Database MYSQL
qua Procedure.

Trong cơ sở hạ tầng cài đặt NUGET package như MySQLConnector, Dapper để kết nối tới dbForge MYSQL