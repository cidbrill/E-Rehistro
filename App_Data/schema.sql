Create Table UserData( 
userId int identity(1,1) primary key not null,
userLast varchar(max) not null,
userFirst varchar(max) not null,
userSuffix varchar(8),
userMiddle varchar(max),
userGender varchar(8) not null,
userBirthday date not null,
userBirthCity varchar(max) not null,
userBirthProvince varchar(max) not null,
userProvince varchar(max) not null,
userCity varchar(max) not null,
userBarangay varchar(max) not null,
userBlknlot varchar(max) not null,
userCitizenship varchar(20) not null,
userDateofNat date,
userCertNo varchar(max),
fatherName varchar(max) not null,
motherName varchar(max) not null,
oath varchar(15) not null,
registered varchar(100) not null
)

create table userInfoPic(
userId int foreign key references UserData(userId),
fileBytes varbinary(max) not null,
fileName varchar(max) not null)

 