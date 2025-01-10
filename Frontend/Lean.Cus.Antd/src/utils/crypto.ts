import CryptoJS from 'crypto-js';

const SECRET_KEY = 'your-secret-key'; // 请替换为实际的密钥

export class CryptoService {
  /**
   * AES 加密
   * @param data 要加密的数据
   * @returns 加密后的字符串
   */
  static encrypt(data: string): string {
    const encrypted = CryptoJS.AES.encrypt(data, SECRET_KEY);
    return encrypted.toString();
  }

  /**
   * AES 解密
   * @param encryptedData 加密的字符串
   * @returns 解密后的数据
   */
  static decrypt(encryptedData: string): string {
    const decrypted = CryptoJS.AES.decrypt(encryptedData, SECRET_KEY);
    return decrypted.toString(CryptoJS.enc.Utf8);
  }

  /**
   * MD5 哈希
   * @param data 要哈希的数据
   * @returns MD5 哈希值
   */
  static md5(data: string): string {
    return CryptoJS.MD5(data).toString();
  }

  /**
   * SHA256 哈希
   * @param data 要哈希的数据
   * @returns SHA256 哈希值
   */
  static sha256(data: string): string {
    return CryptoJS.SHA256(data).toString();
  }

  /**
   * Base64 编码
   * @param data 要编码的数据
   * @returns Base64 编码后的字符串
   */
  static encodeBase64(data: string): string {
    return CryptoJS.enc.Base64.stringify(CryptoJS.enc.Utf8.parse(data));
  }

  /**
   * Base64 解码
   * @param encodedData Base64 编码的字符串
   * @returns 解码后的数据
   */
  static decodeBase64(encodedData: string): string {
    return CryptoJS.enc.Base64.parse(encodedData).toString(CryptoJS.enc.Utf8);
  }

  /**
   * 生成随机密钥
   * @param length 密钥长度（字节）
   * @returns 随机密钥的十六进制字符串
   */
  static generateRandomKey(length: number = 32): string {
    const randomWords = CryptoJS.lib.WordArray.random(length);
    return randomWords.toString();
  }

  /**
   * HMAC-SHA256 签名
   * @param data 要签名的数据
   * @param key 签名密钥
   * @returns HMAC-SHA256 签名
   */
  static hmacSha256(data: string, key: string): string {
    return CryptoJS.HmacSHA256(data, key).toString();
  }
} 