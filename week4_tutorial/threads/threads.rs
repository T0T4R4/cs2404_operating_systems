use std::thread;

fn print_numbers() {
    for i in 1..=5 {
        println!("Number: {}", i);
    }
}

fn print_letters() {
    for letter in "abcde".chars() {
        println!("Letter: {}", letter);
    }
}

fn main() {
    let thread1 = thread::spawn(print_numbers);
    let thread2 = thread::spawn(print_letters);

    thread1.join().expect("Thread 1 panicked");
    thread2.join().expect("Thread 2 panicked");

    println!("Both threads have finished");
}
