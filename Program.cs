 const int NUMBER_OF_COLUMNS = 10;
 const int NUMBER_OF_ROWS = 12;
 const char ROW_START_LETTER = 'A';
 var seats = new Seat[NUMBER_OF_ROWS, NUMBER_OF_COLUMNS];

 ProcessSeats((row, column) => {
    var caption = $"{Convert.ToChar(row + ROW_START_LETTER)}{column + 1:00} ";
    seats[row, column] = new Seat(caption) {
        Status = SeatStatus.Availble
    };
 });

 string? input = null;
 do {
    PrintSeats();
    GotoXY(0,0);
    Console.Write("Enter Book Seat Number :");
    input = Console.ReadLine()!;
    Console.Clear();
    if (int.TryParse(input[1..], out var column)) {
        var row = input[0]- ROW_START_LETTER;
        if(row -1 > NUMBER_OF_ROWS || column - 1 > NUMBER_OF_COLUMNS) {
            PrintError("Row/Column not found");
            continue;
        }
        var seat = seats[row, column - 1];
        seat.Status = SeatStatus.Booked;
    }
    else {
        PrintError("Unknown Seat #");

    }
 } while (!string.IsNullOrWhiteSpace(input));
 
 void GotoXY(int x, int y)
 {
    Console.SetCursorPosition(x, y);
 }

 void PrintSeats()
 {
    GotoXY(0, 2);
    ProcessSeats((row, column) => {
        var seat = seats?[row, column];
        Console.BackgroundColor = seat?.Status == SeatStatus.Availble ? ConsoleColor.Blue : ConsoleColor.Red;
        Console.Write(seat?.Caption);
        Console.ResetColor();
        Console.Write("\t");
    }, Console.WriteLine);
 }

void PrintError(string message)
{
    GotoXY(0, 1);
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"<< {message} >>");
    Console.ResetColor();
}

static void ProcessSeats(Action<int, int> action, Action? separate = null){
    for (var row = 0; row < NUMBER_OF_ROWS; row++) {
        for ( var column = 0; column < NUMBER_OF_COLUMNS; column++) {
            action(row, column);
        }
        separate?.Invoke();
    }
}
Console.ReadKey();

record Seat(string Caption) {
    public SeatStatus Status { get; set; }
}
enum SeatStatus {
    Availble,
    Hold,
    Booked
};
