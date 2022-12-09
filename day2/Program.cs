var lines = File.ReadLinesAsync("input.txt");

var totalStrategy1 = 0;
var totalStrategy2 = 0;
await foreach (var line in lines) {
    var round = ParseRoundStrategy1(line);
    totalStrategy1 += round.Score();

    var round2 = ParseRoundStrategy2(line);
    totalStrategy2 += round2.Score();
}
Console.WriteLine(totalStrategy1);
Console.WriteLine(totalStrategy2);

static Round ParseRoundStrategy1(string line) {
    var choices = line.Split(" ");
    var opponentMove = ParseOpponentMove(choices[0]);
    var playerMove = ParsePlayerMove(choices[1]);
    return new Round(opponentMove, playerMove);
}

static Round ParseRoundStrategy2(string line) {
    var choices = line.Split(" ");
    var opponentMove = ParseOpponentMove(choices[0]);
    var desiredOutcome = ParseOutcome(choices[1]);
    return new Round(opponentMove, CalculatePlayerMode(opponentMove, desiredOutcome));
}

static PlayOption ParseOpponentMove(string input) {
    switch (input) {
        case "A" : return PlayOption.Rock();
        case "B" : return PlayOption.Paper();
        case "C" : return PlayOption.Scissors();
    }
    throw new InvalidDataException("uh oh");
}

static PlayOption ParsePlayerMove(string input) {
    switch (input) {
        case "X" : return PlayOption.Rock();
        case "Y" : return PlayOption.Paper();
        case "Z" : return PlayOption.Scissors();
    }
    
    throw new InvalidDataException("uh oh");
}

static Outcome ParseOutcome(string input) {
        switch (input) {
        case "X" : return Outcome.Lose;
        case "Y" : return Outcome.Draw;
        case "Z" : return Outcome.Win;
    }
    
    throw new InvalidDataException("uh oh");
}

static PlayOption CalculatePlayerMode(PlayOption opponentMove, Outcome desiredOutcome) {
    switch (desiredOutcome) {
        case Outcome.Lose: return PlayOption.FromType(opponentMove.winsAgainst);
        case Outcome.Win: return PlayOption.FromType(opponentMove.losesAgainst);
        default: return opponentMove;
    }
}

class Round {
    private PlayOption opponentMove;
    private PlayOption playerMove;

    public Round(PlayOption opponentMove, PlayOption playerMove)
    {
        this.opponentMove = opponentMove;
        this.playerMove = playerMove;
    }

    public int Score() {
        return playerMove.Value + Score(opponentMove, playerMove);
    }

    private int Score(PlayOption opponent, PlayOption player) {
        var outcome = CalculateOutcome(opponent, player);
        switch (outcome) {
            case Outcome.Win: return 6;
            case Outcome.Draw: return 3;
            default: return 0;
        }
    }

    private Outcome CalculateOutcome(PlayOption opponent, PlayOption player) {
        if (player.winsAgainst == opponent.Type) {
            return Outcome.Win;
        } else if (player.losesAgainst == opponent.Type) {
            return Outcome.Lose;
        } else {
            return Outcome.Draw;
        }
    }
}

class PlayOption {
    public PlayOptionType Type;
    public int Value;
    public PlayOptionType winsAgainst;
    public PlayOptionType losesAgainst;

    public static PlayOption FromType(PlayOptionType type) {
        switch (type) {
            case PlayOptionType.Rock: return PlayOption.Rock();
            case PlayOptionType.Paper: return PlayOption.Paper();
            default: return PlayOption.Scissors();
        }
    }

    public static PlayOption Rock() {
        return new PlayOption {
            Type = PlayOptionType.Rock,
            Value = 1,
            winsAgainst = PlayOptionType.Scissors,
            losesAgainst = PlayOptionType.Paper
        };
    }
    
    public static PlayOption Paper() {
        return new PlayOption {
            Type = PlayOptionType.Paper,
            Value = 2,
            winsAgainst = PlayOptionType.Rock,
            losesAgainst = PlayOptionType.Scissors
        };
    }

    public static PlayOption Scissors() {
        return new PlayOption {
            Type = PlayOptionType.Scissors,
            Value = 3   ,
            winsAgainst = PlayOptionType.Paper,
            losesAgainst = PlayOptionType.Rock
        };
    }

}

enum PlayOptionType {
    Rock,
    Paper,
    Scissors
}

enum Outcome {
    Win,
    Lose,
    Draw
}