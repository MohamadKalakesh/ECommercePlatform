import os

CODE_EXTENSIONS = {'.cs', '.html', '.htm', '.js', '.dockerfile', '.json', '.xml', '.config', '.csproj', '.sln'}
EXCLUDE_FOLDERS = {'bin', 'obj'}

def is_code_file(filename):
    if filename.lower() == 'dockerfile':
        return True
    ext = os.path.splitext(filename)[1].lower()
    return ext in CODE_EXTENSIONS

def print_code_structure(path, indent=""):
    if not os.path.exists(path):
        print(f"Path '{path}' does not exist.")
        return False

    if os.path.isfile(path):
        if is_code_file(os.path.basename(path)):
            print(indent + os.path.basename(path))
            return True
        return False

    code_files_found = False
    files_to_print = []
    dirs_to_print = []

    for item in sorted(os.listdir(path)):
        if item.lower() in EXCLUDE_FOLDERS:
            continue

        item_path = os.path.join(path, item)
        if os.path.isdir(item_path):
            # Check if subfolder contains code files
            if has_code_files(item_path):
                dirs_to_print.append(item)
                code_files_found = True
        else:
            if is_code_file(item):
                files_to_print.append(item)
                code_files_found = True

    if code_files_found:
        print(indent + os.path.basename(path) + "/")
        for file in files_to_print:
            print(indent + "    " + file)
        for dir_name in dirs_to_print:
            print_code_structure(os.path.join(path, dir_name), indent + "    ")

    return code_files_found

def has_code_files(path):
    """Helper function to check if a folder contains code files or subfolders with code files."""
    if not os.path.exists(path):
        return False

    if os.path.isfile(path):
        return is_code_file(os.path.basename(path))

    for item in os.listdir(path):
        if item.lower() in EXCLUDE_FOLDERS:
            continue
        item_path = os.path.join(path, item)
        if os.path.isdir(item_path):
            if has_code_files(item_path):
                return True
        else:
            if is_code_file(item):
                return True
    return False

if __name__ == "__main__":
    folder_path = input("Enter the Visual Studio solution folder path: ").strip()
    print_code_structure(folder_path)
