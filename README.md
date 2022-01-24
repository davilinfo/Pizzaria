--Após gerar base de dados executar script de load em bd
 
use Pizzaria
go

insert into Sabor(nome, valor, disponivel) values('3 Queijos', 50, 1)
insert into Sabor(nome, valor, disponivel) values('Frango com requeijão', 59.99, 1)
insert into Sabor(nome, valor, disponivel) values('Mussarela', 42.50, 1)
insert into Sabor(nome, valor, disponivel) values('Calabresa', 42.50, 1)
insert into Sabor(nome, valor, disponivel) values('Pepperoni', 55, 0)
insert into Sabor(nome, valor, disponivel) values('Portuguesa', 45, 1)
insert into Sabor(nome, valor, disponivel) values('Veggie', 59.99, 0)

go

insert into Cliente(codigo, nome, timestamp, endereco, telefone) 
values('6669ADD57F5D2DF8FAE2CEAE72A4742939EC2A67D8F4FA7681A7EFB1CD84957C', 'cliente1', GETDATE(), 'endereço1', '11111-1111')
insert into Cliente(codigo, nome, timestamp, endereco, telefone) 
values('AABD7242D70A8322E33B50316EB638D3F0055EE6A1C8A4FBF37778B8AC5EB863', 'cliente2', GETDATE(), 'endereço2', '22222-2222')
