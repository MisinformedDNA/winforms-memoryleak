# winforms-memoryleak

Repro:

1. Run app in VS
2. Click "Load records"
3. Wait while 200,000 records are added to a new DB
4. Open "Diagnostic Tools"
5. Click "Open child"
6. When window appears, immediately close it.
7. Observe that the memory is not being releases.
    - If memory is released, return to step 2.
9. Go back to step 5 and repeat as necessary.

NOTE: Garbage collections is happening in a loop at 5 second intervals to help with debugging/dumps.
