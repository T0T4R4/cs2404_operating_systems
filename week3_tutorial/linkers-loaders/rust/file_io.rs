// file_io.rs
use std::fs::File;
use std::io::Write;

fn main() {
    let mut file = File::create("example.txt").expect("Error creating file");

    file.write_all(b"Hello, this is a test.").expect("Error writing to file");
}
