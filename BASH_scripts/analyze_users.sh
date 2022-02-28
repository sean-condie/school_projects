#!/usr/bin/bash

# Analyze the user names within secure logs
#
# Name: analyze_users.sh
# Written by: Sean Condie
# Date: January 26th, 2022
# Purpose: To extract the user names from failled attempts to log into the server from secure log files
# Usage: ./analyze_users.sh #file_name(s)
# Example: ./analyze_users.sh "/home/COIS/3380/secure*"

#
#Verify there is a file name parameter
#
if [[ $1 == *[*] ]]; then #if the parameter has a wildcard
  NAMES=( $1 ) #expand the wildcard
  NAME=${NAMES[0]} #select the first item from the wild card
else #there is no wildcard in the parameter
  NAME=$1 #assign the paramater as the name
fi

#
#Verify the name of the file to be analyzed
#
if [ -z $NAME ]; then 
  echo Please enter the name of the file as a command. #tell the user to enter a file name
  exit 1 #exit the script
elif [ -f $NAME ]; then #if the file name entered exists and is a file
  #search for failed attempts excluding invalid users, extract the username, save to a text file
  grep "Failed password for [^ ]* from"  $1 | awk '{print $9}' > top_ten_target_names.txt
  #search for failed attempts for invalid users, extract the username, concatonate it to the end of the previous text file
  grep "Failed password for invalid user [^ ]* from"  $1 | awk '{print $11}' >> top_ten_target_names.txt
  #sort the usernames, count the unique entries, overwrite the previous text file with only the top ten
  sort top_ten_target_names.txt | uniq -c | sort -n -r | head -n 10 > top_ten_target_names.txt
else #if the file does not exist or is not a file
  echo "The file $1 does not exist" #tell the user the file doesn't exist
  exit 1 #exit the script
fi

#
#Print the results
#
read -p "Would you like to print the results? (y/n) " response #ask the user if they would like to print the results
if [[ "$response" =~ [Yy] ]];then #if they enter 'Y' or 'y' 
  echo The top ten usernames are:
  echo
  cat top_ten_target_names.txt #print the results
  echo
fi

#
#end of script
#