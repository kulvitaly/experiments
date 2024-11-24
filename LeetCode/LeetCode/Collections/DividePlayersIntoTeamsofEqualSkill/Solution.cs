namespace LeetCode.Collections.DividePlayersIntoTeamsofEqualSkill;

public class Solution
{
    public long DividePlayers(int[] skill)
    {
        Array.Sort(skill);

        int targetTeamSkill = skill[0] + skill[skill.Length - 1];
        int chemistry = skill[0] * skill[skill.Length - 1];

        for (int i = 1; i < skill.Length / 2; ++i)
        {
            if (skill[i] + skill[skill.Length - i - 1] != targetTeamSkill)
            {
                return -1;
            }

            chemistry += skill[i] * skill[skill.Length - i - 1];
        }

        return chemistry;
    }
}
