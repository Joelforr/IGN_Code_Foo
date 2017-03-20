create table thumbnails
(id int(3),
url varchar(255),
size varchar(255),
width int(4),
height int(4));

create table apidata
(id int(3),
headline varchar(255),
state varchar(255),
slug varchar(255),
subheadline varchar(255),
publishdate varchar(255),
articletype varchar(255));

create table networks(
id int(3),
network varchar(255));

create table tags(
id int(3),
tag varchar(255));