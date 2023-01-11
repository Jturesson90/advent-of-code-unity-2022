using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JTuresson.AdventOfCode;
using UnityEngine;

namespace AdventOfCode.Days
{
    public class Day07
    {
        public Puzzle7Commands CommandsBuffer { get; set; }
        public Puzzle7ABuffer PuzzleACommandsBuffer { get; set; }

        public struct Puzzle7ABuffer
        {
            public Puzzle7Commands Commands;
            public List<Folder> ResultFolders;
            public List<Folder> AllFolders;
            public long Size;
        }

        public struct Puzzle7Commands
        {
            public List<DirCommand> Commands;
        }

        public enum ECommand
        {
            StepInto,
            StepOut
        }

        public struct DirCommand
        {
            public Folder SelectedFolder;
        }

        public class Folder
        {
            public string name;
            public Folder parent;
            public List<Folder> children;
            public List<Files> files;
            public string path;
            public string directory;
            public int depth;

            public Folder(string n, Folder p)
            {
                children = new List<Folder>();
                files = new List<Files>();
                name = n;
                parent = p;
                if (p == null)
                {
                    depth = 0;
                    directory = "/";
                    path = directory + name;
                }
                else
                {
                    depth = p.depth + 1;
                    directory = $"{p.path}/";
                    path = $"{p.path}/{name}";
                }
            }

            public string GetUniqueName()
            {
                return path;
            }

            public long GetSize()
            {
                long c = 0;
                long start = 0;
                c += files.Aggregate(start, (sum, curr) => sum + curr.size);
                return c + children.Aggregate(start, (sum, curr) => sum + curr.GetSize());
            }

            public bool AddFolder(string n, Folder p)
            {
                if (children.Exists(a => a.name == n))
                {
                    Debug.LogError("Folder " + n + " is already excists");
                    return false;
                }

                children.Add(new Folder(n, p));
                return true;
            }

            public void AddFile(string n, long s)
            {
                if (files.Exists(a => a.name == n))
                {
                    Debug.LogError("File " + n + " is already excists");
                    return;
                }


                files.Add(new Files(n, s, this));
            }

            public Folder GetChild(string n)
            {
                return children.Find(a => a.name == n);
            }

            public bool HasParent()
            {
                return parent != null;
            }
        }

        public class Files
        {
            public string name;
            public long size;
            public Folder parent;

            public Files(string n, long s, Folder p)
            {
                name = n;
                parent = p;
                size = s;
            }
        }

        private Dictionary<string, Folder> Parse(string[] input)
        {
            CommandsBuffer = new Puzzle7Commands()
            {
                Commands = new List<DirCommand>()
            };
            var folders = new Dictionary<string, Folder>();
            Folder startFolder = null;
            Folder currentFolder = null;
            bool lsIsActive = false;

            void AddDir(string name)
            {
                if (currentFolder.AddFolder(name, currentFolder))
                {
                    var uk = currentFolder.GetChild(name).GetUniqueName();
                    if (!folders.ContainsKey(uk))
                    {
                        folders.Add(uk, currentFolder.GetChild(name));
                    }
                    else
                    {
                        Debug.LogError("Already added " + name);
                    }
                }
            }

            void Ls()
            {
                lsIsActive = true;
            }

            void Start()
            {
                startFolder = new Folder("/", null);
                folders.Add("/", startFolder);
                currentFolder = startFolder;
                CommandsBuffer.Commands.Add(new DirCommand()
                {
                    SelectedFolder = startFolder
                });
            }

            void GoBack()
            {
                if (currentFolder != null && !currentFolder.HasParent())
                {
                    Debug.LogError("Cant traverse up anymore, parent is missing");
                }

                currentFolder = currentFolder.parent;
                CommandsBuffer.Commands.Add(new DirCommand()
                {
                    SelectedFolder = currentFolder
                });
            }

            void StepInto(string dir)
            {
                if (currentFolder == null) return;
                var c = currentFolder.GetChild(dir);

                currentFolder = c ?? throw new Exception("Could not step into child");
                CommandsBuffer.Commands.Add(new DirCommand()
                {
                    SelectedFolder = currentFolder
                });
            }

            void AddFile(string n, string s)
            {
                currentFolder.AddFile(n, long.Parse(s));
            }

            foreach (var command in input)
            {
                var commandParts = command.Split(" ");
                bool isCommand = commandParts[0] == "$";
                if (isCommand)
                {
                    lsIsActive = false;
                    if (command == "$ ls")
                        Ls();
                    else
                        if (command == "$ cd /")
                            Start();
                        else
                            if (command == "$ cd ..")
                                GoBack();
                            else
                                if (commandParts.Length == 3 && commandParts[0] == "$" && commandParts[1] == "cd")
                                    StepInto(commandParts[2]);
                                else
                                {
                                    throw new Exception("Unknown command " + command);
                                }
                }
                else
                    if (lsIsActive)
                    {
                        if (commandParts[0] == "dir")
                        {
                            AddDir(commandParts[1]);
                        }
                        else
                        {
                            AddFile(commandParts[1], commandParts[0]);
                        }
                    }
                    else
                    {
                        throw new Exception("Unknown command " + command);
                    }
            }

            return folders;
        }

        public string PuzzleA(string input)
        {
            var newInput = ParseInput.ParseAsArray(input.Trim());
            var folders = Parse(newInput);
            long res = 0;
            foreach (var ke in folders.Values)
            {
                var s = ke.GetSize();
                if (s <= 100000)
                {
                    res += s;
                }
            }

            PuzzleACommandsBuffer = new Puzzle7ABuffer()
            {
                Commands = CommandsBuffer,
                ResultFolders = null,
                AllFolders = folders.Values.ToList(),
                Size = res
            };
            return res.ToString();
        }

        public string PuzzleB(string input)
        {
            var newInput = ParseInput.ParseAsArray(input.Trim());
            var folders = Parse(newInput);
            var startFolder = folders["/"];
            long availableSpace = 70000000;
            long neededSpace = 30000000;
            long missingSpace = Math.Abs(availableSpace - startFolder.GetSize() - neededSpace);
            var f = folders.Where(a => a.Value.GetSize() > missingSpace).Min(a => a.Value.GetSize());


            return f.ToString();
        }
    }
}