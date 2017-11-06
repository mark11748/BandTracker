**Band Tracker : Week4 C# Code Review**  
**_By Mark Woodward_**

**_Description_**  
=================
Basic Management Application to track associated venues with the bands they're hosting.

Specs
==================

* **Homepage displays a list of concert venues**  
* **list of venues:**

|Behavior | Example Input| Example Output|
| ---|:---:| :---:|
|create venue|"some_name"|venue with name "some_name" is created and saved to the venues table in the database|
|get list of all venues|N/a|A list of saved venues is generated|
|find one venue(valid id)|"1"|returns target venue|
|find one venue(invalid id)|"0"|returns 'dummy' venue with id of -1|
|update venue|"newName"|updates the database entry of the venue to match it's current properties/members|
|delete all venues from database|*link is clicked*|all entries in the venue table are deleted and AUTO_INCREMENT is reset to 1|
|delete one venue from database(valid id)|"1"|returns venue whose table id is 1|
|delete one venue from database(invalid id)|"0"|N/a|
|get list of bands who've played at a selected venue|*user clicks name of a venue*|user is redirected to a list of bands who've played at the selected venue|
|schedule a band for a venue|"1"| band 1 is now associated with venue method was called on|

* list of bands  

|Behavior | Example Input| Example Output|
| ---|:---:| :---:|
|create band|"someBand"|band with name "someBand" is created and saved to the bands table in the database|
|record a host venue|"1"| venue with id "1" is now associated with the selected band|
|get all bands|N/a|a list of bands in the database is generated|
|find one band(valid id)|"1"|returns target band|
|find one band(invalid id)|"0"|returns 'dummy' band with id of -1|
|get list of all venues a band has played at|*user clicks band in list*|user is redirected to a list of venues who've hosted the selected band|

**_Installation Instructions_**
===============================  

1. download BandTracker from github repository
2. import sql database into your sql program of choice OR use your sql program with the terminal to create the database manually
  1. /Applications/MAMP/Library/bin/mysql --host=localhost -uroot -proot
  2. CREATE DATABASE band_tracker;
  3. USE band_tracker;
  4. CREATE TABLE bands  (id serial PRIMARY KEY, name VARCHAR(255));
  5. CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255));
  6. CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id INT, venue_id INT);
3. run program using command dotnet run in terminal*

*IMPORTANT: MAKE SURE YOU ARE IN DIRECTORY "BandTracker.Solutions/BandTracker/" before attempting to run. you can check your path in the terminal with the command 'pwd'

**_Technology Used_**
=====================

1. C#
2. cshtml
3. css
4. bootstrap
5. .Net framework
6. MySql
