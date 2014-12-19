$AppPath = "..\KnapsackProblem\bin\Debug\"
$DataPath = "..\Data\KnapsackProblem\"

& $AppPath"KnapsackProblem.exe" -p 	$DataPath"velky" `
								-r 	$DataPath"velky-sol.dat"  `
								-g -i 100 -m 0.9 -n 0.9 -q elitary -t 100 -w 0 -z 