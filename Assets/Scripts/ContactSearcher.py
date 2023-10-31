import csv
import os
from pathlib import Path

info = []

def format_zip(str_zip):
    if len(str_zip) == 4:
        final_zip = "0" + str_zip
        return final_zip

def load_sheet():
    global reader
    global all_rows

    with open(os.path.join(os.getcwd(), "Assets\\Scripts\\contacts.csv"), "r", encoding="utf8") as csvfile:
        reader = csv.reader(csvfile)
        all_rows = list(reader)

    with open(os.path.join(os.getcwd(), "passthrough.txt"), "r", encoding="utf8") as passthrough:
        passthrough_reader = csv.reader(passthrough)
        full_name = list(passthrough_reader)

    for i in all_rows:
        if i[5] == full_name[0][0]:
            print("Found Match for First Name!")
            if i[6] == full_name[0][1]:
                print(f"Found {i[5], i[6]}!")
                break

    with open(os.path.join(os.getcwd(), "passthrough.txt"), mode='w', newline='') as file:
        writer = csv.writer(file, delimiter=',', quotechar='"', quoting=csv.QUOTE_MINIMAL)

        info.append(full_name[0][0])
        info.append(full_name[0][1])
        info.append(i[17])
        info.append(i[18])
        info.append(i[19])
        info.append(format_zip(i[20]))
        info.append(i[25])
        info.append(i[28])

        # Write the CSV data to the file
        writer.writerow(info)

load_sheet()