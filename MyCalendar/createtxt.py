import datetime

# define start and end dates
start_date = datetime.date(2023, 1, 1)
end_date = datetime.date(2045, 12, 31)
delta = datetime.timedelta(days=1)


dates = []
while start_date <= end_date:
    dates.append(start_date.strftime('%Y,%m,%d'))
    start_date += delta

print(dates[0])

start_date2 = datetime.date(1990, 1, 1)
end_date2 = datetime.date(2022, 12, 31)

while start_date2 < end_date2:
    dates.append(end_date2.strftime('%Y,%m,%d'))
    end_date2 -= delta


# open a file for writing
with open('datestodata.txt', 'w') as f:
    # iterate over all dates and write them to the file
    for date in dates:
        f.write(date + ',\n')

