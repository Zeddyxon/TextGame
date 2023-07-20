/*

//TAKTO se debuguje dle hozny
 
Console.WriteLine("x: " + randX1 + ", y: " + randY1);
Console.WriteLine("x: " + randX2 + ", y: " + randY2);
Console.WriteLine("lenY: " + returnSector.Count());

-> also je dobre si zjednodusit citelnost kodu tim ze si rozpisu veci do proměnných


//DVEŘE by honza
                switch (rooms.IndexOf(room))
                {
                    case 0:
                        randX1 = room.TopCornerX + room.width;
                        randY1 = rand.Next(room.TopCornerY, room.TopCornerY + room.height);

                        randX2 = rand.Next(room.TopCornerX, room.TopCornerX + room.width);
                        randY2 = room.TopCornerY + room.height;

                        room.doorLeftRight = mapList[randY1][randX1];
                        room.doorUpDown = mapList[randY2][randX2];
                        break;
                    case 1:
                        randX1 = room.TopCornerX;
                        randY1 = rand.Next(room.TopCornerY, room.TopCornerY + room.height);

                        randX2 = rand.Next(room.TopCornerX, room.TopCornerX + room.width);
                        randY2 = room.TopCornerY + room.height;

                        room.doorLeftRight = mapList[randY1][randX1];
                        room.doorUpDown = mapList[randY2][randX2];
                        break;
                    case 2:
                        randX1 = room.TopCornerX + room.width;
                        randY1 = rand.Next(room.TopCornerY, room.TopCornerY + room.height);

                        randX2 = rand.Next(room.TopCornerX, room.TopCornerX + room.width);
                        randY2 = room.TopCornerY;

                        room.doorLeftRight = mapList[randY1][randX1];
                        room.doorUpDown = mapList[randY2][randX2];
                        break;
                    case 3:
                        randX1 = room.TopCornerX;
                        randY1 = rand.Next(room.TopCornerY, room.TopCornerY + room.height);

                        randX2 = rand.Next(room.TopCornerX, room.TopCornerX + room.width);
                        randY2 = room.TopCornerY;

                        room.doorLeftRight = mapList[randY1][randX1];
                        room.doorUpDown = mapList[randY2][randX2];
                        break;
                }


*/