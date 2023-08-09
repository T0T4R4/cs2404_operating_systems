use std::sync::Arc;
use std::sync::atomic::{AtomicBool, Ordering};
use std::thread;
use std::time::Duration;
use std::ptr;
use libc::c_void;

fn main() {
    // Specify the size and key for shared memory
    let size = 1024;
    let key = 1234;

    // Create a shared memory segment
    let shmid = unsafe {
        libc::shmget(key, size, libc::IPC_CREAT | 0o666)
    };

    if shmid == -1 {
        panic!("Failed to create shared memory segment");
    }

    // Attach the shared memory segment to a pointer
    let shmptr = unsafe {
        libc::shmat(shmid, ptr::null(), 0)
    };

    if shmptr == ptr::null_mut() {
        panic!("Failed to attach shared memory segment");
    }

    // Data to be written to shared memory
    let data = "Hello from Rust!";
    let cstr = std::ffi::CString::new(data).unwrap();

    // Copy the data into shared memory
    unsafe {
        ptr::copy_nonoverlapping(cstr.as_ptr() as *const c_void, shmptr, data.len());
    }

    println!("Data written to shared memory");

    // Clean up by detaching the shared memory segment
    unsafe {
        libc::shmdt(shmptr);
    }
}
