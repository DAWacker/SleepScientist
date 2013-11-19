using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;

namespace SleepyScientist
{
    class LevelLoader
    {

        #region Methods

        public static Room Load(int level)
        {
            try
            {
                // Set these values to anything
                Room room = null;
                int numFloors = 0;
                int startFloor = 0;
                int curFloorNum = 0;
                List<Floor> floors = new List<Floor>();
  
                
                // Load in any of the levels
                XmlTextReader reader = new XmlTextReader("Content/Levels/Level" + level + ".xml");
                reader.WhitespaceHandling = WhitespaceHandling.None;

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            switch (reader.Name)
                            {
                                case "Info":
                                    reader.Read();
                                    reader.Read();
                                    numFloors = Int32.Parse(reader.Value);
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    startFloor = Int32.Parse(reader.Value);
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    int startX = Int32.Parse(reader.Value);
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    int startY = Int32.Parse(reader.Value);
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    int startDirection = Int32.Parse(reader.Value);
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    int bedX = Int32.Parse(reader.Value);
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    int bedY = Int32.Parse(reader.Value);
                                    Bed bed = new Bed(bedX-50, bedY, GameConstants.BED_WIDTH, GameConstants.BED_HEIGHT);
                                    bed.Image = GameConstants.BED_TEXTURE;
                                    room = new Room(numFloors, startFloor, startX, startY, startDirection, bed);
                                    break;
                                case "Floor":
                                    reader.Read();
                                    reader.Read();
                                    int xcoor = Int32.Parse(reader.Value);
                                    reader.Read();
                                    reader.Read();
                                    reader.Read();
                                    int ycoor = Int32.Parse(reader.Value);
                                    Floor floor = new Floor(xcoor, ycoor, GameConstants.SCREEN_WIDTH, GameConstants.FLOOR_HEIGHT);
                                    floor.Image = GameConstants.FLOOR_TEXTURE;
                                    room.Floors.Add(floor);
                                    curFloorNum++;
                                    while (reader.Name != "Floor" && reader.Read())
                                    {
                                        switch (reader.NodeType)
                                        {
                                            case XmlNodeType.Element:
                                                switch (reader.Name)
                                                {
                                                    case "Teleporter":
                                                        reader.Read();
                                                        reader.Read();
                                                        int teleporterXcoor = Int32.Parse(reader.Value);
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int teleporterYcoor = Int32.Parse(reader.Value);
                                                        Teleporter teleporter = new Teleporter(teleporterXcoor, teleporterYcoor, GameConstants.TELEPORTER_WIDTH, GameConstants.TELEPORTER_HEIGHT + GameConstants.TILE_HEIGHT);
                                                        teleporter.Tile = GameConstants.TELEPORTER_TILE_TEXTURE;
                                                        teleporter.Top = GameConstants.TELEPORTER_TOP_TEXTURE;
                                                        teleporter.Bottom = GameConstants.TELEPORTER_BOTTOM_TEXTURE;
                                                        floor.Teleporters.Add(teleporter);
                                                        break;

                                                    case "Stairs":
                                                        reader.Read();
                                                        reader.Read();
                                                        int stairXcoor = Int32.Parse(reader.Value);
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int stairYcoor = Int32.Parse(reader.Value);
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int stairDirection = Int32.Parse(reader.Value);
                                                        Stairs stairs = new Stairs(stairXcoor, stairYcoor, GameConstants.STAIR_WIDTH, GameConstants.STAIR_HEIGHT, stairDirection);
                                                        stairs.Image = GameConstants.STAIR_TEXTURE;
                                                        stairs.RailingTexture = GameConstants.RAILING_TEXTURE;
                                                        floor.Stairs.Add(stairs);
                                                        break;

                                                    case "Pit":
                                                        reader.Read();
                                                        reader.Read();
                                                        int pitXcoor = Int32.Parse(reader.Value);
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int pitYcoor = Int32.Parse(reader.Value);
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int length = Int32.Parse(reader.Value);
                                                        Pit pit = new Pit(pitXcoor, pitYcoor, GameConstants.TILE_WIDTH * length, GameConstants.TILE_HEIGHT);
                                                        pit.LeftEnd = GameConstants.PIT_LEFT_END_TEXTURE;
                                                        pit.RightEnd = GameConstants.PIT_RIGHT_END_TEXTURE;
                                                        pit.Tile = GameConstants.PIT_TILE_TEXTURE;
                                                        pit.Terminal = GameConstants.PIT_TERMINAL_TEXTURE;
                                                        floor.Pits.Add(pit);
                                                        break;

                                                    case "Door":
                                                        reader.Read();
                                                        reader.Read();
                                                        int doorXcoor = Int32.Parse(reader.Value);
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int doorYcoor = Int32.Parse(reader.Value);
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int doorTime = Int32.Parse(reader.Value);
                                                        Door door = new Door(doorXcoor, doorYcoor, GameConstants.DOOR_WIDTH, GameConstants.DOOR_HEIGHT, doorTime);
                                                        door.Image = GameConstants.DOOR_OPEN_TEXTURE;
                                                        room.Door = door;
                                                        break;

                                                    case "Invention":
                                                        reader.Read();
                                                        reader.Read();
                                                        string inventionType = reader.Value;
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int inventionXcoor = Int32.Parse(reader.Value);
                                                        reader.Read();
                                                        reader.Read();
                                                        reader.Read();
                                                        int inventionYcoor = Int32.Parse(reader.Value);
                                                        switch (inventionType)
                                                        {
                                                            case "JackInTheBox":
                                                                JackInTheBox box = new JackInTheBox("jackie", inventionXcoor, inventionYcoor, GameConstants.TILE_WIDTH, GameConstants.TILE_HEIGHT, room, curFloorNum);
                                                                box.Image = GameConstants.JACK_TEXTURE;
                                                                floor.Inventions.Add(box);
                                                                break;
                                                            case "EggBeater":
                                                                EggBeater egg = new EggBeater("beatMe", inventionXcoor, inventionYcoor + GameConstants.EGGBEATER_HEIGHT, GameConstants.EGGBEATER_WIDTH, GameConstants.EGGBEATER_HEIGHT, room, curFloorNum);
                                                                egg.Image = GameConstants.EGG_TEXTURE;
                                                                floor.Inventions.Add(egg);
                                                                break;
                                                            case "RocketSkateboard":
                                                                RocketSkateboard board = new RocketSkateboard("board", inventionXcoor, inventionYcoor + GameConstants.SKATEBOARD_HEIGHT, GameConstants.SKATEBOARD_WIDTH, GameConstants.SKATEBOARD_HEIGHT, room, curFloorNum);
                                                                board.Image = GameConstants.ROCKETBOARD_TEXTURE;
                                                                floor.Inventions.Add(board);
                                                                break;
                                                            case "Batteries":
                                                                break;
                                                        }
                                                        break;

                                                    default:
                                                        break;
                                                }
                                                break;
                                        }
                                    }
                                    break;

                                default:
                                    break;
                            }
                            break;
                    }
                }
                return room;
            }
            catch
            {
                Console.WriteLine("No file found.");
                return null;
            }
        }

        #endregion
    }
}
