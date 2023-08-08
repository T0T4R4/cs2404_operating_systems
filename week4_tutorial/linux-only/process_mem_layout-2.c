// Adding one global variable should increase the memory allocated by the Initialized data segement
//
// Memory allocation profile
//    text    data     bss     dec     hex filename
//    1228     548       4    1780     6f4 process_mem_layout-2

#include<stdio.h>

long a = 1, b = 2, c = 4; // sizeof(global_variable) == 4 bytes

int main() {
    return 0;
}
