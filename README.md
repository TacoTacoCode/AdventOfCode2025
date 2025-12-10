# AdventOfCode2025

# Day 1

We can think that the arrow rotate is actually a cursor moving on a straight line, and all the mark 100, 200, -100, -200 are representing for 0 mark.

#### Part 1

-   Just calculate the final position of the cursor, if it is either -100, 100, 200.. means the arrow is actually stop at 0

=> count when remainder after divided by 100 is 0

#### Part 2

-   Simply add count every time we cross those 0 representers, include case where we cross 0 and minus 1 if it stops at a representers (since we already count it)

---

# Day 2

#### Part 1

-   Simply check left half equal right half

#### Part 2

-   It is a common question in Math problem (I guess) - how to know if a string is a repeated pattern
    -   One way is use very popular check, if that string is repeated, we just need to append it self and remove head tail to eliminate the chance it repeat as left half or right half. If that new string contain the original, it is a repeated pattern
        ```
        (s + s).Substring(1, 2 * s.Length - 2).Contains(s)
        ```
    -   The other way, which is what i use, is just pure brute force, find all posible pattern and build the new string to check if it is same with original.

---
