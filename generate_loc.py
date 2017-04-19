# python3 generate_loc.py -i <file> -o <outfile>

import sys, getopt, os.path
from ODSReader import ODSReader


def main(argv):
    inputFilename = ''
    outputFilename = ''
    numberOfLanguages = 0;

    try:
        opts, args = getopt.getopt(argv,"hi:o:",["ifile=","ofile="])
    except getopt.GetoptError:
        print ('File error: test.py -i <inputfile> -o <outputfile>')
        sys.exit(2)

    for opt, arg in opts:
        if opt == '-h':
            print ('test.py -i <inputfile> -o <outputfile>')
            sys.exit()
        elif opt in ("-i", "--ifile"):
            inputFilename = arg
        elif opt in ("-o", "--ofile"):
            outputFilename = arg


    if os.path.isfile(inputFilename):
        print('Found file:', inputFilename)
    else:
        print('Unable to find file:', inputFilename)
        sys.exit()


    # Open our output file...
    outputFile = open(outputFilename, 'w')
    print('Opened file: ', outputFilename)

    # Open input spreadsheet and grab sheet 1
    inputFile = ODSReader(inputFilename)
    sheet1  = inputFile.getSheet(u'Sheet1')
    print ('Opened input file, starting build...')
    numberOfLanguages = int(sheet1[0][0]);
    # Build output string
    outputFile.write('using UnityEngine;\nusing System.Collections;\nusing System;\n\npublic static partial class LocaleStrings\n{\n')
    outputFile.write('\tprivate static string[] CurrentLocale;\n\n')
	
    for j in range (1, numberOfLanguages+1):
        outputFile.write('\tpublic static readonly string[] '+sheet1[0][j]+' = {\n' )
        for i in range (1, len(sheet1)-1):
            outputFile.write('\t\t\"'+ sheet1[i][j] + '\",\n')
        outputFile.write('\t\t\"'+ sheet1[len(sheet1)-1][j] + '\"\n\t};\n\n')

    for i in range (1, len(sheet1)-1):
        if (sheet1[i][0] != ""):
            outputFile.write('\tpublic static string ' + sheet1[i][0]+' { get { return CurrentLocale['+str(i-1)+']; } }\n\n')    

    outputFile.write('\tpublic enum StringsEnum : int\n\t{')
    for i in range (1, len(sheet1)-1):
        if (sheet1[i][0] != ""):
            outputFile.write('\n\t\t' + sheet1[i][0] + '=' + str(i-1) + ',')
    outputFile.write('\n\t}\n')
    outputFile.write('}\n')

    outputFile.close()

if __name__ == "__main__":
    main(sys.argv[1:])