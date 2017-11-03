* Homepage displays a list of concert venues
==================

* @ list of venues
==================

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
|schedule a band for a venue|||

* @ list of bands
==================

|Behavior | Example Input| Example Output|
| ---|:---:| :---:|
|create band|"someBand"|band with name "someBand" is created and saved to the bands table in the database|
|get all bands|N/a|a list of bands in the database is generated|
|find one band(valid id)|"1"|returns target band|
|find one band(invalid id)|"0"|returns 'dummy' band with id of -1|
|get list of all venues a band has played at|*user clicks band in list*|user is redirected to a list of venues who've hosted the selected band|
|record a host venue|||

mark_woodward.bands :
CREATE DATABASE band_tracker;
USE band_tracker;
CREATE TABLE bands  (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE bands_venues (id serial PRIMARY KEY, band_id INT, venue_id INT);
