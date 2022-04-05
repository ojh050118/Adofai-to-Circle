namespace AdofaiToCircle
{
    public static class FileCopyHelper
    {
        /// <summary>
        /// 복사를 시도해봅니다.
        /// </summary>
        /// <param name="sourceFileName">파일경로가 포함된 원본 파일이름.</param>
        /// <param name="destFileName">파일경로가 포함된 복사할 파일이름.</param>
        /// <returns>복사 성공 여부.</returns>
        public static bool TryCopy(string sourceFileName, string destFileName)
        {
            try
            {
                if (File.Exists(sourceFileName))
                    File.Copy(sourceFileName, destFileName);
                else
                    return false;

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
