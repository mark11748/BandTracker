

@ list of venues
--------------------
create venue
get list of all venues
find one venue
update venue
delete all venues from database
delete one venue from database
get list of bands who've played at a selected venue
schedule a band for a venue

@ list of bands
------------------------
create band
get all bands
find one band
get list of all venues a band has played at
record a host venue

mark_woodward.bands :
CREATE DATABASE band_tracker;
GO USE band_tracker;
CREATE TABLE bands  (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE venues (id serial PRIMARY KEY, name VARCHAR(255));
CREATE TABLE bands_venues (serial PRIMARY KEY, band_id INT, venue_id INT);
