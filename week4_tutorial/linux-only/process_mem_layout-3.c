// Adding an UNinitialized static variable should increase the memory occupied by the Uninitialized data space (BSS)
//
// Memory allocation profile:
//    text    data     bss     dec     hex filename
//    1228     544       8    1780     6f4 process_mem_layout-3
#include<stdio.h>

static long a, b, c, d;

int main() {
    return 0;
}
