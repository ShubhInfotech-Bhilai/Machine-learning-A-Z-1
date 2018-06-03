# Importing the libraries
import numpy as np # Math library
import matplotlib.pyplot as plt #Plotting library
import pandas as pd # Import/manage dataset library

# Importing the dataset using pandas. In this case our dataset is a CVS file
dataset = pd.read_csv('Position_Salaries.csv')

############## Read independant variables #################
# Now we are going to select all our independant variables. In this case our
# independent variables are position and level

# iloc - first parameter are the rows of the dataset, the second parameter
# are the columns. -1 means we don't take the last column, because the last
# column is our dependant variable.
X = dataset.iloc[:, 1:2].values

############## Read dependant variables #################
# In this dataset our only dependant variable is the second column
# which contains the salary
Y = dataset.iloc[:, 2].values