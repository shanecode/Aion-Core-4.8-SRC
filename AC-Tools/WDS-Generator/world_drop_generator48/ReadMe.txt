WDS Aion-Core 4.8

- to reduce the svn storage place, the WDS doesn't include the Item and NPC templates. You will need to add them from your local pc from the repository

world_drop_generator48\data\static_data\items\
item_templates.xml
item_templates.xsd

world_drop_generator48\data\static_data\npcs
npc_templates.xml
npcs.xsd

=======================
Changes and commits
=======================

Please commit just changed or added codes, after you generated the drops and just if everythig is working as intended.
However, please do not commit the generated drops, such npc_drop.dat and npc_drops.xml.
Make sure that you heve deleted the two files before you committ your adds or changes.
We are interessted just in templates changes, everyone can generate the drop files for self.