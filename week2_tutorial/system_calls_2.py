# This scripts illustrate the usage of system calls in Python by executing a shell command.

import os

def execute_shell_command(command):
    os.system(command)

if __name__ == "__main__":
    command = "dir"  # Change this to the desired shell command
    execute_shell_command(command)
