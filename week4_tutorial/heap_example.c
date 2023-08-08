// simple code which allocates some memory for a single character on the help
//

#include <stdio.h>
#include <stdlib.h>  // Include the necessary header for malloc and free

int main()
{
    int* p;

    printf("Press Enter to allocate memory...\n");
    while (getchar() != '\n'); // Wait for Enter key

    p=(int*)malloc(10000000 * sizeof(int));    /* memory allocating in heap segment */

    printf("Press Enter to free memory and terminate...\n");
    while (getchar() != '\n'); // Wait for Enter key

    free(p); // Free the allocated memory to prevent memory leaks
    return 0;
}
