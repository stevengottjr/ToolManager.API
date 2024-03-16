# Tool Manager API [^1]
[^1]: This project is my first real foray into proper development using C#! I'm sure it is far from perfect and as I get further along, I am certain that numerous necessary changes will become apparent. 

## Purpose:
I currently work in manufacturing engineering and oversee programming for multiple CNC mills. One of my biggest headaches is keeping track of various part numbers for production tooling. Machines can have anywhere from a dozen to hundreds of tools loaded into their ATC magazines. Solid tooling is fairly easy; a part number for the tool, and a part number for the holder. However, indexable tools might have multiple inserts and accompanying screws in addition to the tool body and holder. Additionally, depending on the spindle taper of the machine, you might use the same tool in a different holder. When trying to support multiple machines in a production environment, this turns into a lot of information to try and keep organized. This was the reason that I wanted to develop this API!

## Core objects:
- Machine : Each machine has an: id, name, and taper
- Tool : Each tool has an: id, manufacturer, part number, description, usable length, cutting diameter, shank diameter, and tooth count
- Holder : Each holder has an: id, manufacturer, part number, description, taper, shank diameter, and projection
- Insert : Each insert has an: id, manufacturer, part number, description, and an FK for the toolid that consumes it
- Screw : Each screw has an: id, description, and an FK for the toolid that consumes it

## Complex objects:
- ToolAssembly : Each tool assembly has an: id, an FK for the holderid, and an FK for the toolid
- MachineTool : A machine tool has an: id, an FK for the machineid, and an FK for the toolassemblyid

## Database:
SQLite is used as the database. ToolMgr.db is included for testing purposes. Intended path is user\appdata\local\ToolMgr.db

## Code:You Capstone Project Requirements:
My project is a RESTful asyncronous CRUD API. In total the database has 7 tables and creates relationships between them as described above. I intend to build a proper frontend for this API using Vue.js or similar in the future, but in order to submit the project by the due date, all testing should be done using Swagger. 


