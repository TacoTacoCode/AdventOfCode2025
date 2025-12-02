# AdventOfCode2025

# Day 1

We can think that the arrow rotate is actually a cursor moving on a straight line, and all the mark 100, 200, -100, -200 are representing for 0 mark.

#### Part 1

-   Just calculate the final position of the cursor, if it is either -100, 100, 200.. means the arrow is actually stop at 0

=> count when remainder after divided by 100 is 0

#### Part 2

-   Simply add count every time we cross those 0 representers, include case where we cross 0 and minus 1 if it stops at a representers (since we already count it)

---
