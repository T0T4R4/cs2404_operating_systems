// file_io.c
#include <stdio.h>

int main() {
    FILE* file = fopen("example.txt", "w");
    if (file == NULL) {
        perror("Error opening file");
        return 1;
    }

    fputs("Hello, this is a test.", file);

    fclose(file);
    return 0;
}
