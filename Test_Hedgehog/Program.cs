
class Program
{
    static int[][] pairs = new int[][]
    {
        [0, 1],
        [0, 2],
        [1, 2]
    };

    public static int MinMeetings(int[] population, int target)
    {
        if ((population[0] > 0 ? 1 : 0) + (population[1] > 0 ? 1 : 0) + (population[2] > 0 ? 1 : 0) == 1)
            return population[target] > 0 ? 0 : -1;

        var visited = new HashSet<string>();
        var queue = new Queue<(int[] state, int steps)>();

        queue.Enqueue((population, 0));
        visited.Add(string.Join(",", population));

        while (queue.Count > 0)
        {
            var (state, steps) = queue.Dequeue();

            if (state[0] > 0 && state[1] == 0 && state[2] == 0 && target == 0) return steps;
            if (state[1] > 0 && state[0] == 0 && state[2] == 0 && target == 1) return steps;
            if (state[2] > 0 && state[0] == 0 && state[1] == 0 && target == 2) return steps;

            foreach (var p in pairs)
            {
                int a = p[0], b = p[1];
                int c = 3 - a - b; 

                if (state[a] > 0 && state[b] > 0)
                {
                    var newState = (int[])state.Clone();
                    newState[a]--; newState[b]--;
                    newState[c] += 2;

                    string key = string.Join(",", newState);
                    if (!visited.Contains(key))
                    {
                        visited.Add(key);
                        queue.Enqueue((newState, steps + 1));
                    }
                }
            }
        }

        return -1;
    }

    static void Main()
    {
        int[] population = new int[] { 8, 1, 9 };
        int targetColor = 2; 

        int result = MinMeetings(population, targetColor);
        Console.WriteLine(result);
    }
}
