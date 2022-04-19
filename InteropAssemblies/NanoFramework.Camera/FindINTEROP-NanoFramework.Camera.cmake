#
# Copyright (c) .NET Foundation and Contributors
# See LICENSE file in the project root for full license information.
#

########################################################################################
# make sure that a valid path is set bellow                                            #
# this is an Interop module so this file should be placed in the CMakes module folder  #
# usually CMake\Modules                                                                #
########################################################################################

# native code directory
set(BASE_PATH_FOR_THIS_MODULE ${PROJECT_SOURCE_DIR}/InteropAssemblies/NanoFramework.Camera)


# set include directories
list(APPEND NanoFramework.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Core)
list(APPEND NanoFramework.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/CLR/Include)
list(APPEND NanoFramework.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/HAL/Include)
list(APPEND NanoFramework.Camera_INCLUDE_DIRS ${PROJECT_SOURCE_DIR}/src/PAL/Include)
list(APPEND NanoFramework.Camera_INCLUDE_DIRS ${BASE_PATH_FOR_THIS_MODULE})


# source files
set(NanoFramework.Camera_SRCS

    NanoFramework_Camera.cpp


    NanoFramework_Camera_NanoFramework_Camera_Camera_mshl.cpp
    NanoFramework_Camera_NanoFramework_Camera_Camera.cpp

)

foreach(SRC_FILE ${NanoFramework.Camera_SRCS})
    set(NanoFramework.Camera_SRC_FILE SRC_FILE-NOTFOUND)
    find_file(NanoFramework.Camera_SRC_FILE ${SRC_FILE}
        PATHS
	        ${BASE_PATH_FOR_THIS_MODULE}
	        ${TARGET_BASE_LOCATION}
            ${PROJECT_SOURCE_DIR}/src/NanoFramework.Camera

	    CMAKE_FIND_ROOT_PATH_BOTH
    )
    # message("${SRC_FILE} >> ${NanoFramework.Camera_SRC_FILE}") # debug helper
    list(APPEND NanoFramework.Camera_SOURCES ${NanoFramework.Camera_SRC_FILE})
endforeach()

include(FindPackageHandleStandardArgs)

FIND_PACKAGE_HANDLE_STANDARD_ARGS(NanoFramework.Camera DEFAULT_MSG NanoFramework.Camera_INCLUDE_DIRS NanoFramework.Camera_SOURCES)
