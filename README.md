Poker Hand Sorter - .NET CORE Console App

The application was built using .NET Core and C#. To run the application open the project in visual studio and either run it in VS using the debugger or please build it by right clicking on the project and selecting "Build". Apologies for not pushing the executable files to the git repository. Executable files with necessary dependencies will be generated in the PokerHandSorter\PokerHandSorter\bin\Debug\netcoreapp3.0 folder. 

Please include the text file which you want the Poker Hand Sorter to use in the PokerHandSorter\PokerHandSorter\bin\Debug\netcoreapp3.0 folder when running the application using visual studio. Also, to set the name of the text file as an application argument when running the application using the visual studio debugger right click on the PokerHandSorter project and go to Properties and then Debug. Enter the name of the text file, for example, poker-hands.txt, in the application arguments field.

If you are running it using the .exe file please include the text file in the same folder as the executable file. To run the application using the executable file open the command prompt, navigate to the location of the executable file and run the .exe file passing the text file name along with it. For example, PokerHandSorter.exe poker-hands.txt. Please note once you build the project or publish it, all the files generated in the respective folder, PokerHandSorter\PokerHandSorter\bin\Debug or PokerHandSorter\PokerHandSorter\bin\Release, are required to run the application in the command prompt. only the .exe file will not work as it's dependencies are missing.

The hands won by player 1 and 2 will be printed and if by any chance there were unbroken ties, the count of this will be printed as well.

Please note when evaluating for a royal flush the application will check for Ten, Jack, Queen, King and Ace only, not the order it is in, as the task specification doesn't mention it should be in consecutive order.