# C# library for working with zoned-decimal

This library was created out of the need to interface with a legacy IBM code page 037 (EBCDIC) based mainframe hosted by a large financial organization. In particular, these systems use a method of representing numeric values known as "zoned-decimal" that harkens back to punch card days.

## What is Zoned-Decimal?
We'll try to make this brief, but the explanation is somewhat involved. Also, all of this pre-dates me and I have tried to piece together what I can from research. Feel free to chime in with any corrections.

IBM punch cards consisted of 12 rows. The top two rows were numbered 12 & 11, but were generally unlabeled on the card. The remaining rows were numbered 0 through 9 and appeared below the top two (does anyone know why they changed from a zero-based numbering system on rows 0 through 9 to a 1-based system for rows 11 & 12?). Characters, which were encoded with the EBCDIC character set, were divided into groups or "zones". The first zone consisted of the characters A through I and was indicated by punching row 12 on the card in combination with the numeric position of the character you wanted from the zone (e.g. punch 1 for A, 2 for B, etc). Zone 2 consisted of the characters J through R. To get a character from zone two, you would punch row 11 in combination with the numeric position of the character you wanted (e.g. 1 for J, 2 for K, etc). Zone 3 was indicated by punching row 0 and likewise the numeric position of the character you wanted. 

![punch card](https://github.com/jbhensley/zoned-decimal/blob/master/img/IBMPunchCard.jpg)

Unsigned numeric digits were indicated by simply punching their value on rows 0 through 9 and leaving rows 12 & 11 unpunched. A signed positive digit was indicated by punching row 12 and the corresponding number below, essentially making signed positive numbers 1 through 9 identical to the characters A through I. Signed negative digits were indicated by punching row 11 and the corresponding number below, essentially making signed negative numbers 1 through 9 identical to the characters J through R.

The EBCDIC characters A through I all consist of the bit pattern 0xC in their zone and a numeric value of 1 through 9 in their digit, while J through R used 0xD for the zone. Unsigned numeric characters have 0xF for the zone.

![code page 037](https://github.com/jbhensley/zoned-decimal/blob/master/img/ibm037.png)

That means that the bit pattern for signed positives, which correspond to the characters A through I, look like this:

- A = 0xC1 = 1100 0001
- B = 0xC2 = 1100 0010
- C = 0xC3 = 1100 0011

etc. The bit pattern for signed negatives, which correspond to the characters J through R, look like this:

- J = 0xD1 = 1101 0001
- K = 0xD2 = 1101 0010
- L = 0xD3 = 1101 0011

etc. And the unsigned:

- 1 = 0xF1 = 1111 0001
- 2 = 0xF2 = 1111 0010
- 3 = 0xF3 = 1111 0011

etc. While it's not evident from the example punch card above, zeros were apparently represented using the same zone bits with a digit value of zero. That is:

- signed pos zero = 0xC0 = 1100 0000 = character '{'
- signed neg zero = 0xD0 = 1101 0000 = character '}'
- unsigned zero = 0xF0 = 1111 0000 = character '0'

If your number consisted of a series of digits instead of just one then only the very last digit carried the sign while all of the rest were marked as unsigned. To punch the number -12.73 (negative 12.73), for example, you would punch the equivalent of:

- 1 = 0xF1 = 1111 0001
- 2 = 0xF2 = 1111 0010
- 7 = 0xF7 = 1111 0111
- L = 0xD3 = 1101 0011

With the final digit carrying the sign (negative in this case).

## Converting Zoned-Decimal

One approach to converting between modern, signed numerics and zoned-decimal essentially consists of translating the right-most character from/into the appropriate sign and numeric value that it represents. A naive and easy to code implementation would be to simply call ToString() on numeric values and then perform text inspection and replacement functions to transform characters to their zoned-decimal representation and vice versa. However, string manipulation is very slow compared to numeric calculations and one of the objectives of this library is to perform quickly.

Our method of conversion involves calculating the zoned-decimal value of each digit and/or character. While the code is more involved than simple string manipulation, we take advantage of the fact that computers are very efficient at performing bitwise operations and integer calculations. By avoiding string manipulation and/or string concatenation, we are able to build a very small library that simply converts values back and forth without suffering from a massive performance hit.

