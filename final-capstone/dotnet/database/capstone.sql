USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,

	CONSTRAINT PK_user PRIMARY KEY (user_id)
)

CREATE TABLE focuses (
	focus_id int IDENTITY(1,1) NOT NULL,
	focus_name varchar(50) NOT NULL,

	CONSTRAINT PK_focuses PRIMARY KEY (focus_id)
)

CREATE TABLE exercises (
	exercise_id int IDENTITY(1,1) NOT NULL,
	exercise_name varchar(50) NOT NULL,
	focus_id int NOT NULL,
	description varchar(200) NOT NULL,
	weight varchar(50),
	time varchar(50) NOT NULL,
	repetition int

	CONSTRAINT PK_exercises PRIMARY KEY (exercise_id)
	constraint fk_exercises_focus foreign key (focus_id) references focuses(focus_id)
)

CREATE TABLE userExercises (
	user_id int NOT NULL,
	exercise_id int NOT NULL,

	constraint fk_userExercises_user foreign key (user_id) references users(user_id),
	constraint fk_userExercises_exercise foreign key (exercise_id) references exercises(exercise_id)
)





--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');

GO