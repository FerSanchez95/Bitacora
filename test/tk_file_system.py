from tkinter import filedialog

# Display the dialog for browsing files.
filename = filedialog.askopenfilename()
# Print the selected file path.
print(filename)
