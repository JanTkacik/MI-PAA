$AppPath = "..\KnapsackProblem\bin\Debug\"
$DataPath = "..\Data\KnapsackProblem\"

& $AppPath"KnapsackProblem.exe" -p 	$DataPath"knap_40.inst.dat" `
								-r 	$DataPath"knap_40.sol.dat"  `
								-g -i 100 -m 0.9 -n 0.9 -q roulette -t 30 -w 0 