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
                           description varchar (1000) NOT NULL,
                           weight int,
                           time int NOT NULL,
                           repetitions int,
                           sets int,
						   user_id int NOT NULL

                               CONSTRAINT PK_exercises PRIMARY KEY (exercise_id),
                               constraint fk_exercises_focus foreign key (focus_id) references focuses(focus_id),
							   constraint fk_exercises_user foreign key (user_id) references users(user_id)
)

--purpose of this table is to store exercises for user (what they currently have)
CREATE TABLE userExercises (
                               user_id int NOT NULL,
                               exercise_id int NOT NULL,

                               constraint fk_userExercises_user foreign key (user_id) references users(user_id),
                               constraint fk_userExercises_exercise foreign key (exercise_id) references exercises(exercise_id)
)

CREATE TABLE workoutHistory (
								user_id int NOT NULL,
								time int NOT NULL,
								date date NOT NULL

								constraint fk_workoutHistory_user foreign key (user_id) references users(user_id),
)



--populate default data
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('brock','lMEIO2mSYs7UiXA4SkUxoMTOFeI=', 'mvW9u2Op5wE=','admin');

INSERT INTO focuses (focus_name)
VALUES ('cardio')

INSERT INTO focuses (focus_name)
VALUES ('back')

INSERT INTO focuses (focus_name)
VALUES ('legs')

INSERT INTO focuses (focus_name)
VALUES ('arms')

INSERT INTO focuses (focus_name)
VALUES ('abs')

INSERT INTO exercises (exercise_name, focus_id, description, weight, time, repetitions, sets, user_id)
VALUES ('Butt Kicks', 1, 'Standing tall, you are going to alternate, kicking yourself in the butt.',null,30, null, null, 2),
	   ('Dumbbell Runs', 1, 'Place a dumbbell (a towel or a water bottle) in front of you and run a circle around it. ',null, 30, null,null,3),
       ('Soccer Drill Toe Taps', 1, 'Place a ball in the front of you, keep your hips forward, lifting up your knees, tap the top of the ball with the ball of your foot, then jump switch and tap the ball with the opposite foot.',null,30, null,null,2),
       ('Speed Rope', 1, 'Simulate jumping rope as fast as you possibly can. Keep your feet close together and rotating your arms rapidly.',null,30, null,null,2),
	   ('Mountain Climbers', 1, 'Bring your knee up to your chest, jump the foot in front of the ground in front of you, bring it back to the start position and repeat on the opposite side. ',null,30, null,null,3),
       ('High Knees', 1, 'High knees is similar to jogging in place but with greater range of motion. Tilt your pelvis up and forward and bring your knees as high as you can.',null, 30, null,null,2),
       ('Burpees', 1, 'Squat down, placing your hands on the floor in front of you, thrust legs back behind you, lowering your torso and your chest all the way to the ground. Then press back up jumping forward into a squat position then jumping all the way up, repeat.',null,30, null,null,2),
       ('Suicides', 1, 'Imagine a line drawn in front of you, run to the end of the line, squat down and touch the ground. Then run back the other way and touch the other end of the line. Continue repeating this runs as fast as possible.',null,30, null,null,3),
       ('Speed Bag', 1, 'In a circular motion, pretend pounding on a speed bag at the gym ',null,30, null,null,2),
       ('Cross Over Jacks', 1, 'Crossing arms over the body, legs over the body, opposite arm, opposite leg every time.',null,30, null,null,2),
--Back
	   ('TRX row', 2, 'Grab hold of the TRX handles and walk under them, forming a tabletop position with your arms extended. The more parallel your back is to the ground, the harder this exercise will be.
		 Keeping your back straight, row upward by pulling yourself toward the ceiling. Keep your elbows close to your sides.
	     Extend your arms and return to start, ensuring that your hips do not sag.',null, 120, 12,3,3),
	   ('Wood chop', 2, 'Grab the dumbbell or medicine ball with both hands. Hold it above your head with your arms extended. Pivot on your right foot slightly so your hips are rotated.
		 As you begin to squat down, rotate your hips to the left and bring the dumbbell or ball down to the outside of your left knee in a sweeping movement.
		 On the ascent, twist your trunk back toward the right and, keeping your arms straight, bring the dumbbell or ball back up above the right side of your head in an explosive but controlled movement. This movement should mimic a chopping motion, hence the name.',null, 120, 12,3,2),
       ('Renegade dumbbell row', 2, 'Assume a high plank position with each of your hands on a dumbbell. Your body should form a straight line from your head to your toes. Your core should be engaged throughout the movement.
         Row with your right arm, pulling your elbow toward the sky while keeping it close to your body, then returning the dumbbell to the ground. Ensure that your hips stay square to the ground. Repeat with your left arm.',null, 120, 20,3,3),
       ('Single-arm dumbbell row', 2, 'Position yourself on a bench so your left knee and shin are resting on it, as well as your left hand � this will be your support. Your right leg should be straight with your foot on the ground. Pick up the dumbbell with your right hand. Maintain a straight torso.
         Row the dumbbell up, pulling your elbow toward the sky while keeping it close to your body. Squeeze your upper back as you pull your elbow up.
         Slowly lower back down to the start position.',null, 120, 12,3,3),
       ('Hyperextension', 2, 'Lie down on an exercise ball with your abdomen on the center of the ball. Press the balls of your feet into the ground to stay balanced.
		 Extend your arms forward. Bending at your waist, slowly raise your upper body toward the sky. Be sure to engage your core and glutes. Keep your feet on the floor.
		 Pause for a moment when at the top, then slowly lower down.',null, 120, 12,3,3),
       ('Barbell deadlift', 2, 'Stand behind the barbell with your feet shoulder-width apart.
		 Keeping your chest lifted, begin to hinge at the hips and slowly bend your knees, reaching down to pick up the barbell. Keep your back straight and grasp the bar with both palms facing you in an overhand grip.
		 Push back up, keeping your feet flat on the floor, back into the starting position. Your back should remain straight throughout the movement. Your shoulders should be down and back.
		 Return to the starting position, pushing your hips back and bending your knees until you bring the barbell back to the ground.',null, 120, 12,3,2),
       ('Wide dumbbell row', 2, 'Hold a dumbbell in each hand and hinge at the waist, stopping when your upper body forms a 20-degree angle with the ground. Your palms should be facing your thighs, and your neck should remain neutral. Allow the dumbbells to hang down in front of you.
		 Begin to row with your elbows at a 90-degree angle, pulling them up toward the sky. Squeeze your shoulder blades together at the top.
		 Return to start and repeat',null, 120, 12,3,3),
	   ('Lat pulldown', 2, 'If you�re using a machine, position the pad so it�s touching your thighs. Stand up and grab the bar wider than shoulder-width apart, sitting back down.
		 Begin to pull the bar down toward your chest, bending your elbows and directing them down to the ground. Engage your upper and mid back throughout this whole movement. Keep your torso straight, not allowing yourself to fall backward.',null, 120, 12,3,2),
	   ('Resistance band pull apart', 2, 'Stand with your arms extended. Hold a resistance band taut in front of you with both hands so the band is parallel to the ground.
	     Keeping your arms straight, pull the band to your chest by moving your arms out to your sides. Initiate this movement from your mid back, squeezing your shoulder blades together and keeping your spine straight, then slowly return to start.',null, 120, 12,3,2),
	   ('Quadruped dumbbell row', 2, 'Get on all fours with a dumbbell positioned in each hand. Ensure your back is straight, hands are directly below shoulders, and knees are directly below hips.
		 Row up with your right arm, pulling your elbow up and bringing the dumbbell to your armpit. Keep your elbow tucked throughout the movement. You will notice here that if you row too far, you will lose your balance.
		 Extend your arm, returning the dumbbell to the ground, and repeat on the left side.', null, 120, 12,3,3),
--Legs
       ('Squats', 3, ' Stand with your feet at shoulder width and your toes turned slightly out. Take a deep breath and bend your hips back, then bend your knees to lower your body as far as you can without losing the arch in your lower back. Push your knees out as you descend. Drive vertically with your hips to come back up, continuing to push your knees out. To increase difficulty, hold dumbbells in each hand in line with your shoulders.',null,240, 15, 3,2),
	   ('Step ups', 3, 'Stand behind a bench or another elevated surface that will put your thigh at parallel to the floor when you step your foot onto it. Step onto the bench, but leave your trailing leg hanging off. To increase difficulty, hold dumbbells in each hand at your sides.',null,180, 20, 2,2),
	   ('Calf-Raises', 3, 'Stand with your toes on a block and hold onto something sturdy for support. Raise your heels to come up on the balls of your feet, and then lower your heels until you feel a stretch in your calves. To increase difficulty, hold dumbbells in each hand at your sides.',null,180, 20, 3,3),
	   ('Walking Lunges', 3, 'Stand with your feet hip width. Step forward with one leg and lower your body until your rear knee nearly touches the floor and your front thigh is parallel to the floor. Step forward with your rear leg to perform the next rep. To increase difficulty, hold dumbbells in each hand at your sides.',null,240, 10, 3,3),
	   ('Leg Press', 3, 'Adjust the seat of the machine so that you can sit comfortably with your hips beneath your knees and your knees in line with your feet. Remove the safeties and lower your knees toward your chest until they�re bent 90 degrees and then press back up. Be careful not to go too low or you risk your lower back coming off the seat (which can cause injury).',60, 300, 10, 3,2),
	   ('Single-Leg Romanian Deadlift', 3, 'Hold a dumbbell in one hand and stand on the opposite leg. Bend your hips back and lower your torso until you feel your lower back is about to lose its arch. Squeeze your glutes and extend your hips to come up.',10,180, 10, 2,2),
	   ('Barbell Hip Thrust', 3, 'Rest your upper back on a bench and sit on the floor with legs extended. Roll a loaded barbell up your thighs until the bar sits on your lap (you may want to place a towel or mat on your hips or attach a pad to the bar for comfort). Brace your abs and drive your heels into the floor to extend your hips, raising them until your thighs and upper body are parallel to the floor.',20,120, 10, 2,2),
	   ('Swiss Ball Wall Squat', 3, 'Place the ball against a wall and stand with your back against it, holding it in place. Place your feet shoulder-width apart and turn your toes out about 15 degrees. Squat down as low as you can, rolling the ball down the wall as you descend. To increase difficulty, hold dumbbells in each hand at your sides.',null,240, 10, 3,2),
       ('Single-Leg Glute Bridge', 3, 'Lie on your back on the floor and bend both knees so that your feet rest on the floor close to your butt. Brace your abs and raise one leg up and bring the knee toward your chest. Drive the heel of the other foot into the floor. Bridge up until your body is in a straight line.',null,180, 10, 2,3),
	   ('Swiss Ball Leg Curl', 3, 'Brace your abs with heels on a stability ball. Raise your hips into the air, but keep your knees straight. From there, bend your knees and roll the ball back toward you. Keep your hips elevated throughout the set',null,180, 10, 2,3),
--arms
	   ('Dumbbell Curl', 4, 'Start with your knees slightly bent and your feet about hip-width apart, grasp a pair of dumbbells with an underhand grip. Let the dumbbells hang at the sides of your thighs. Move without swaying, slowly curl the dumbbells in an arc toward your shoulders. Pause at the top of the movement, squeeze your biceps and slowly lower the weights to the start.',10,300, 12, 3,2),
       ('Incline Dumbbell Curl', 4, 'Start by grasping a pair of dumbbells and lie back on an incline bench set at about 60 degrees, allowing your arms to hang straight down toward the floor by your sides. Use an underhand grip, with your palms facing forward. While keeping your shoulders back and upper arms in a fixed position perpendicular to the floor, lock your elbows at your sides and curl both dumbbells toward your shoulders. Slowly return the dumbbell to the start.',8,300, 12, 3,2),
       ('Barbell Preacher Curl', 4, ' Secure your elbows onto the bench and grasp the barbell with both hands. using the bend for leverage curl the bar upward in an arching fashion squeezing the biceps at the top and slowly return back down to the starting position and complete for reps.',20,240, 10, 3,2),
	   ('Triceps Pressdown', 4, 'Start with a slight bend in your knees, stand facing a high-cable pulley with a pressdown bar attached to it. Your feet should be spread about shoulder-width apart. Grasp the pressdown bar with an overhand grip and hold the bar short at chest level with your elbows tight against your sides.
		 While keeping your elbows stationary, straighten your arms until they are fully extended. Pause at full arm extension and flex your triceps, then slowly return the bar to the start position.',45,300, 15, 3,3),
       ('Triceps Dip', 4, 'Find a dip apparatus with relatively narrow grips (no wider than shoulder width). Begin holding onto the bars with your body vertical to the floor and your arms extended. Bend your arms to slowly lower yourself down, keeping your body vertical throughout. When your elbows reach roughly 90 degrees, press yourself back up to the start position.
		',null,300, 12, 3,2),
	   ('Lying Triceps Extension', 4, 'Lay down on a bench and take a weight bar in your hands keeping your hands shoulder width apart, with your arms outstretched and straight in front of your face slowly bend your elbows keeping your elbows in alignment and inward lower the bar down to just above the crown of your head until your forearms are parallel to the floor then straighten your arms above your face and contract your triceps.',25,300, 12, 3,2),
	   ('Standing Barbell Military Press', 4, 'Take a barbell and bring it up to your clavicle or use a smith machine for assistance, start by keeping your elbows down at your sides then press the bar straight up until your arms are completely stretched out above your head. Come down slowly keeping your arms slightly in front of your shoulders the entire time throughout the exercise.',40,300, 10, 3,3),
	   ('Dumbbell Lateral Raise', 4, ' Stand with your feet about shoulder width apart while holding two dumbbells at your sides with neutral grips. Slowly raise the dumbbells up and out to your sides keeping your arms straight with a very slight bend in your elbows. When your arms reach just above parallel with the floor pause in t his position for a second before slowing lowering the dumbbells back to the start.',8,300, 12, 3,3),
       ('Standing Dumbbell Shrugs', 4, 'Take both weights in your hands. Shrug your shoulders up and back squeezing the traps as you come up.',40,300, 15, 3,3),
	   ('Dumbbell Wrist Curl', 4, ' Sit at the end of a flat bench with your legs in front of you and feet flat on the floor, spaced hip-width apart. While holding a dumbbell with an underhand grip in your right hand, rest your right forearm on top of your right thigh so that your wrist and hand hang off your knee. Extend your wrist back so that your hand hang down from your wrist at about a 90 degree angle. The dumbbell should be supported with just your fingers. Curl the weight up, starting with your fingers, then your wrist, until your wrist is flexed and your hand is as high past parallel with the floor as possible. Hold this position for a second while forcefully contracting your forearm muscles, then slowly return the dumbbell back to the start in the reverse manner. Complete the desired number of reps and repeat with the left arm.',8,300, 15, 3,2),
--abs
       ('Plank', 5, 'Your weight should be on the balls of your feet and your elbows, with your hands locked together in front. Keep your back and hips aligned so you form a straight line from your shoulders to your ankles. Brace your abdominals, and do not raise or lower those hips.',null,60, null, null,3),
	   ('Mountain Climber', 5, 'Drop into a top press-up position, supporting your weight on your hands and toes, with your arms straight and your legs extended. Keeping your core braced and your shoulders, hips and feet in a straight line throughout, bring one knee towards your chest, then return it to the starting position. Repeat the movement with your other leg, then continue alternating legs throughout.',null,60, null, null,2),
	   ('Reverse Crunch', 5,
         N'Start lying down with your arms by your sides. Raise your legs so your thighs are perpendicular to the floor and your knees are bent at a 90� angle. Breathe out and contract your abs to bring your knees up towards your chest and raise your hips off the floor. Hold for a beat in this position, then slowly lower your legs back to the starting position.', null, 60, 15, null,2),
       ('Russian Twist', 5, 'Start by sitting on the floor, with your knees bent and feet flat on the ground. Then lean back so your upper body is at a 45-degree angle to the floor. Keep your back straight at this angle throughout the exercise, as it will be tempting to hunch your shoulders forward. Link your hands together in front of your chest, then brace your core and raise your legs up off the ground. Rotate your arms all the way over to one side, then do the same in the other direction.',null,60, 20, null,2),
       ('Dead Bug', 5, 'Lie flat on your back with your arms held out in front of you pointing to the ceiling. Then bring your legs up so your knees are bent at 90-degree angles. Slowly lower your right arm and left leg at the same time, exhaling as you go. Keep going until your arm and leg are just above the floor, being careful not to raise your back off the ground. Then slowly return to the starting position and repeat with the opposite limbs.',null, 180, 10, 3,2),
       ('Bird Dog', 5, 'Start on all fours, much like a regular dog, although you should be on your knees rather than your feet. Stretch your right arm out in front of you, hold for a moment to get your balance, then extend your left leg behind you. Keep your neck in line with your spine, your hips level and the extended limbs straight. From your bird-dog position bring your extended limbs back to the starting position, then extend out again and repeat, aiming for five reps in total before you switch to the opposite limbs.',null,180, 5, 3,2),
       ('Medicine Ball Crunch', 5, 'Start on the ground, with your knees bent and the medicine ball held against your chest. Extend your arms out above you in a straight line. Keep them straight as you perform a sit up, and then reach back as you come down, so the medicine ball touches the ground behind your head.', 10, 240, 15, 3,3),
	   ('L-Sit', 5,
         N'Sit with your palms on the floor by your sides. Point your fingers forwards and spread them out as if you�re trying to grip the ground. Brace your legs, abs and glutes and lift yourself off the ground. Leaning your torso forwards slightly will help with your balance. Then hold that position.', null, 10, null, null,3),
	   ('Hollow Body Rock', 5,
         N'Start lying on your back. Maintain this position throughout the whole movement. Bend your knees and place your feet on the floor. Begin by lifting your shoulders off the floor, and at the same time lift your knees and feet off the floor to a 90� angle. While maintaining this position with your back pressed into the ground, start to rock back and forth.', null, 20, null, null,2),
       ('Bicycle Crunch', 5, 'Start by lying on the ground, with your lower back pressed flat into the floor and your head and shoulders raised slightly above it. Place your hands lightly on the sides of your head; do not knit your fingers behind. Lift one leg just off the ground and extend it out. Lift the other leg and bend your knee towards your chest. As you do so twist through your core so the opposite arm comes towards the raised knee. Your elbow should stay in same position relative to your head throughout. Lower your leg and arm at the same time while bringing up the opposite two limbs to mirror the movement.', null, 180, 10, 3,2)









GO