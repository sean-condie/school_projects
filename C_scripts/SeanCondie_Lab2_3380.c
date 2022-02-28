/*====================================================================== 
|   
|
|   Name: SeanCondie_Lab2_3380.c
|
|   Written by: Sean Condie - February 16th, 2022
|
|   Purpose: Prints the details of the Largest, Smallest, Oldest Modified, and Earliest Modified file in a directory
|
|        
|   Compile: gcc -o SeanCondie_Lab2_3380 SeanCondie_Lab2_3380.c
|
|   usage:  ./SeanCondie_Lab2_3380 [directory path]
|
|   Description of parameters:
|      [directory path] - optional parameter that lists the directory to be processed.
|
|
|   Subroutines/libraries required:
|      
|      See include statements.
|
|
|------------------------------------------------------------------*/

/* 
List of sources used:

https://stackoverflow.com/questions/1271064/how-do-i-loop-through-all-files-in-a-folder-using-c
https://stackoverflow.com/questions/2431242/c-warning-conflicting-types
https://cboard.cprogramming.com/c-programming/72671-displaying-file-permissions.html
https://pubs.opengroup.org/onlinepubs/009696799/functions/stat.html
http://www.cplusplus.com/reference/ctime/difftime/
https://stackoverflow.com/questions/31633943/compare-two-times-in-c

Example programs found at: /home/COIS/3380/sample_code

file_example.c
file_example_details.c
dir_example.c
5_read_file_example.c

*/

#include <sys/stat.h>   /* structures to store file info */
#include <dirent.h>     /* Structures for directory entries */      
#include <string.h>     /* string structures */
#include <stdio.h>      /* Standard I/O functions */
#include <stdlib.h>     /* Prototypes of commonly used library functions */
#include <time.h>       /* allows to compare times */

#define PATH_LENGTH 128 /* maximum pathlength */

void Print(struct stat file, char path[PATH_LENGTH])
{
    /* print permissions: code adapted from https://cboard.cprogramming.com/c-programming/72671-displaying-file-permissions.html */
    int fileMode;
    /* print a leading dash as start of file/directory permissions */
        printf("-");
        fileMode = file.st_mode;
        /* Check owner permissions */
        if ((fileMode & S_IRUSR) && (fileMode & S_IREAD))
          printf("r");
        else
          printf("-");
        if ((fileMode & S_IWUSR) && (fileMode & S_IWRITE)) 
          printf("w");
        else
          printf("-");
        if ((fileMode & S_IXUSR) && (fileMode & S_IEXEC))
          printf("x");
        else
          printf("-");
        /* Check group permissions */
        if ((fileMode & S_IRGRP) && (fileMode & S_IREAD))
          printf("r");
        else
          printf("-");
        if ((fileMode & S_IWGRP) && (fileMode & S_IWRITE))
          printf("w");
        else
          printf("-");
        if ((fileMode & S_IXGRP) && (fileMode & S_IEXEC))
          printf("x");
        else
          printf("-");
        /* check other user permissions */
        if ((fileMode & S_IROTH) && (fileMode & S_IREAD))
          printf("r");
        else
          printf("-");
        if ((fileMode & S_IWOTH) && (fileMode & S_IWRITE))
          printf("w");
        else
          printf("-");
        if ((fileMode & S_IXOTH) && (fileMode & S_IEXEC))
          /* because this is the last permission, leave 3 blank spaces after print */
          printf("x   ");
        else
          printf("-   ");


    /* print the rest of the details */
    printf("%d | ", file.st_uid);
    printf("%d | ", file.st_gid);
    printf("%d | ", file.st_size);
    printf("%s | ", ctime(&file.st_mtime));
    printf("%s\n", path);
}

int main( int argc, char *argv[] )
{   
    /* to store the various full paths we need*/
    char dir_path[PATH_LENGTH];
    char file_path[PATH_LENGTH];

    char oldest_file_path[PATH_LENGTH];
    char youngest_file_path[PATH_LENGTH];
    char largest_file_path[PATH_LENGTH];
    char smallest_file_path[PATH_LENGTH];

    /* stores the information for each file we are interested in */
    struct stat oldest;
    struct stat youngest;
    struct stat largest;
    struct stat smallest;

    /*pointer for the directory */
    DIR *dp;

    /*pointer for the opened directory */
    struct dirent *dir;

    /* used to initialize the files with the first file we open */
    int is_first_file = 0;

    /* are there 2 arguments? */
    if ( argc == 2 ) 
    {
        strcpy(dir_path, argv[1]); /* then store the second argument as the directory path */
    } 
    else if ( argc == 1 ) 
    {
        strcpy(dir_path, "."); /* then store the current directory (.) as the directory path */
    }
    else /* invalid arguments */
    {
        fprintf( stderr, "Too many parameters, only specify the directory\n" );
        exit(1);
    }

    /* verify the directory */
    if ( (dp = opendir(dir_path)) == NULL ) 
    {
        fprintf( stderr, "Cannot open dir\n" ); 
        exit(1);
    }
    
    while ((dir = readdir(dp)) != NULL )                     /* loop through the files in the directory */
    {
        
        if (strcmp(dir->d_name, ".") == 0 || strcmp(dir->d_name, "..") == 0)  continue;  /* Skip . and .. */
        double timediff;
        
        struct stat current;                                 /* create a stat structure to store the current file */
        sprintf( file_path , "%s/%s",dir_path,dir->d_name) ; /* combine the directory path with the current filename to generate a full path */

        if( stat(file_path, &current) != 0)                  /* try to call stat on the full file path, and store the result in current */
        {
            printf("Unable to stat file: %s\n",file_path) ;  /* if stat returns anything other than zero then skip this file */
            continue ;
        }

        if ((current.st_mode & S_IFMT) == S_IFDIR)           /* check if the current full path is a directory */
        {
            /* This is a Directory */
            
            continue; /* Skip */
        }
        else if (is_first_file == 0)                         /* if it is the first file (0 is true, else is false) */
        {
            oldest = current;                                /* Then initialize all files */
            youngest = current;
            largest = current;
            smallest = current;                                
            strcpy(oldest_file_path, file_path);
            strcpy(youngest_file_path, file_path);
            strcpy(largest_file_path, file_path);
            strcpy(smallest_file_path, file_path);
            is_first_file = 1; /* set to 1 to skip this step for all other files */ 
        }
        else                                                 /* file is valid, analyze */ 
        {
            /* logic for largest file */
            if (largest.st_size < current.st_size)           /* if largest file is smaller than the current file */ 
            {
                largest = current;                           /* set the largest file to the current file */
                strcpy(largest_file_path, file_path);        /* set the file path */
            }

            /* logic for smallest file */
            if (smallest.st_size > current.st_size)           /* if smallest file is larger than the current file */ 
            {
                smallest = current;                           /* set the smallest file to the current file */ 
                strcpy(smallest_file_path, file_path);        /* set the file path */ 
            }

            /* logic for youngest file */
            if (difftime(current.st_mtime, youngest.st_mtime) > 0.0) /* current mtime - youngest mtime, positive means current is younger */ 
            {
                youngest = current;                           /* set the youngest file to the current file */ 
                strcpy(youngest_file_path, file_path);        /* set the file path */ 
            }

            /* logic for oldest file */
            if (difftime(current.st_mtime, oldest.st_mtime) < 0.0)  /* current mtime - oldest mtime, negative means current is older */ 
            {
                oldest = current;                             /* set the oldest file to the current file */ 
                strcpy(oldest_file_path, file_path);          /* set the file path */ 
            }

        }

    }

    /* print the results of each */
    printf("The largest file:\n");
    Print(largest, largest_file_path);

    printf("The smallest file:\n");
    Print(smallest, smallest_file_path);

    printf("The oldest file:\n");
    Print(oldest, oldest_file_path);

    printf("The youngest file:\n");
    Print(youngest, youngest_file_path);

    /* close the directory */
    closedir(dp);

    return 0;
}