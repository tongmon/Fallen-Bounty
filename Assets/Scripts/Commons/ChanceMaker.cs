// 확률 발생기
public static class ChanceMaker
{
    // ChanceMaker.GetChanceResult(30) -> 30퍼 확률로 true 반환
    public static bool GetChanceResult(float chance)
    {
        if (chance < 0.0000001f)
            chance = 0.0000001f;

        bool success = false;
        int rand_accuracy = 10000000;
        float rand_hit_range = chance * rand_accuracy;
        int rand = UnityEngine.Random.Range(1, rand_accuracy + 1);
        
        if (rand <= rand_hit_range)
            success = true;

        return success;
    }

    // ChanceMaker.GetChanceResultPercentage(1.0f/10.0f) -> 10분의 1 확률로 true 반환
    public static bool GetChanceResultPercentage(float percentage_chance)
    {
        if (percentage_chance < 0.0000001f)
            percentage_chance = 0.0000001f;

        percentage_chance = percentage_chance / 100;

        bool success = false;
        int rand_accuracy = 10000000;
        float rand_hit_range = percentage_chance * rand_accuracy;
        int rand = UnityEngine.Random.Range(1, rand_accuracy + 1);
        
        if (rand <= rand_hit_range)
            success = true;

        return success;
    }
}
