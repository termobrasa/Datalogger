create database Client

use Client 

create table Product
(
id int primary key,
name varchar(20)
)

insert into Product values(1,'aaa')
insert into Product values(2,'bbb')
insert into Product values(3,'ccc')

select * from Product