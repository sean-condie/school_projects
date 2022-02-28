#!/usr/bin/bash

# assignment submission tool that zips directories
#
# Name: assignment_submission_tool.sh
# Written by: Sean Condie
# Date: January 26th, 2022
# Purpose: To zip all the files within a specified directory
# Usage: ./assignment_submission_tool.sh #directory_name
# Example: ./assignment_submission_tool.sh "lab0"

#
#check if there is a command input
#
if [ -z $1 ]; then #if the first command variable is empty
 echo Please enter the name of the directory as a command. #send a message to the user
 exit 1 #exit the script
fi
#
#everything past here can assume $1 is not NULL
#
NAME=$1 #store the name
#
#get the full path of the specified directory
#
DIR=`find $HOME/ -type d -name "$NAME" -print`
#check that it exists
if [ -z $DIR ]; then #if the directory does not exist
 echo The specified directory, $NAME, does not exist within $HOME #tell the user the directory does not exist
 exit 1 #exit the program
fi
#
#everything past here can assume the directory exists
#
ZIPNAME=${USER}_${NAME}.zip #name of the desired zip
#
#show the user what will happen and ask if they would like to proceed
#
read -p "You are about to zip $DIR to $HOME/$ZIPNAME, would you like to continue? (y/n) " response
echo #move to a new line
if [[ "$response" =~ [Yy] ]];then #if they answer 'Y' or 'y'
 zip $HOME/$ZIPNAME $DIR/* #zip the directory
else #if they dont answer 'Y' or 'y'
 exit 1 #exit the script
fi
#
#end of script
#

