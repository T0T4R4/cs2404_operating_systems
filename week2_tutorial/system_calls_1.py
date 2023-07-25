# This scripts illustrate the usage of system calls in Python by reading the content of a file.

import os

def read_file(filename):
    try:
        with open(filename, 'r') as file:
            content = file.read()
            print(f"Content of {filename}:\n{content}")
    except FileNotFoundError:
        print(f"File '{filename}' not found.")

if __name__ == "__main__":
    filename = "example.txt"  # Change this to the desired file
    read_file(filename)
